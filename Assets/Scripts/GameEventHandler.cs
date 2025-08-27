using Mechanics;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class GameEventHandler : MonoBehaviour
{
    #region Singleton
    public static GameEventHandler Instance;
    #endregion

    #region Public References
    [Header("Component References")]
    public AudioSource ZoneSoundEffectSource;
    public Volume volume;
    public SceneMgr sceneManager;
    public Animator SpotLight;

    [Header("Audio Clips")]
    public AudioClip ZoneCountdown;
    public AudioClip ZoneGameOverSound;

    [Header("Scene Lighting")]
    public List<Light> LightsToToggle = new List<Light>();
    #endregion

    #region Configuration
    [Header("Game Configuration")]
    public float speed = 0.4f;
    public float exposureStartValue = 3f;
    public float exposureEndValue = 0.3f;
    #endregion

    #region State Management
    [Header("Game State")]
    public string currentGameMode = "LANE";
    public Game currentZoneGame;
    public bool IsExposureReduced = false;

    // Dictionary mapping a lane number (0 for Zone) to its running state.
    public Dictionary<int, bool> isLaneRunning = new Dictionary<int, bool>();

    // Caches the payload received during the "GAME_COUNTDOWN" state for each lane.
    public Dictionary<int, JObject> PayloadDuringCountdown = new Dictionary<int, JObject>();

    // Stores the game type for each active lane.
    public Dictionary<int, GameDefinition.GameType> gameTypesForLanes = new Dictionary<int, GameDefinition.GameType>();

    // Stores the current state string (e.g., "IDLE", "GAME_RUNNING") for each lane.
    public Dictionary<int, string> CurrentGameState = new Dictionary<int, string>();
    #endregion

    #region Unity Lifecycle
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Initialize game states for all possible lanes (0 is Zone, 1-4 are Lanes)
        for (int i = 0; i <= 4; i++)
        {
            CurrentGameState.Add(i, "IDLE");
            isLaneRunning.Add(i, false);
        }
    }
    #endregion

    #region Event Handlers

    /// <summary>
    /// Handles incoming events from the game controller.
    /// This is the primary entry point for state changes.
    /// </summary>
    /// <param name="payload">The JSON payload containing the event data.</param>
    public void HandleReply(JObject payload)
    {
        string command = payload["command"]?.ToString();
        if (command == "LANE_FINISHED")
        {
            HandleLaneFinished(payload);
            return;
        }

        if (command == "UPDATE_BACKGROUND")
        {
            int backgroundId = int.Parse(payload["backgroundId"].ToString());
            sceneManager.UpdateBackground(backgroundId);
            return;
        }

        // Ensure all players mentioned in the payload exist locally, fetching them if necessary.
        EnsurePlayersExist(payload["playerPayloads"] as JArray, "playerId");
        EnsurePlayersExist(payload["leaderboard"] as JArray, "playerId");

        string gameMode = payload["gameMode"]?.ToString();
        switch (gameMode)
        {
            case "LANE":
                HandleLaneMode(payload);
                break;
            case "ZONE":
            case "GAUNTLET":
                HandleZoneMode(payload);
                break;
            case "DEMO":
                DemoManager.instance.EnterDemoMode();
                break;
        }

        checkAndControlCart();
        UpdatePostProcessingEffects();
    }

    /// <summary>
    /// Handles events related to target interactions (start, hit, timeout).
    /// </summary>
    /// <param name="payload">The JSON payload from the target event.</param>
    public void HandleTargetEvents(JObject payload)
    {
        Debug.Log($"Target Event: {payload["command"]} | Device: {payload["data"]["deviceId"]}");
        string command = payload["command"].ToString();

        if (currentGameMode == "LANE")
        {
            int laneNumber = int.Parse(payload["tag"].ToString().Split('_')[1]);
            if (laneNumber == 0) return;

            string deviceId = payload["data"]["deviceId"].ToString();

            switch (command)
            {
                case "TARGET_START":
                    TargetManager.Instance.StartTargetNew(laneNumber, deviceId);
                    break;
                case "TARGET_HIT":
                    AudioManager.Instance.PlayExplosion();
                    TargetManager.Instance.HitTargetNew(laneNumber, deviceId);
                    UpdatePlayerScore(payload, laneNumber);
                    break;
                case "TARGET_TIMEOUT":
                    TargetManager.Instance.StopTargetNew(laneNumber, deviceId);
                    break;
            }
        }
        else if (currentGameMode == "ZONE")
        {
            string deviceId = payload["data"]["deviceId"].ToString();
            switch (command)
            {
                case "TARGET_START":
                    TargetManager.Instance.StartTarget(deviceId, 0);
                    break;
                case "TARGET_HIT":
                    AudioManager.Instance.PlayExplosion();
                    TargetManager.Instance.HitTarget(deviceId);
                    UpdatePlayerScore(payload, 0); // 0 for Zone
                    break;
                case "TARGET_TIMEOUT":
                    TargetManager.Instance.StopTarget(deviceId);
                    break;
            }
        }
    }
    #endregion

    #region Game Control

    /// <summary>
    /// Sends a command to the socket to officially start the game for a specific lane or zone.
    /// </summary>
    /// <param name="laneNumber">The lane to start (0 for Zone).</param>
    public void StartGame(int laneNumber)
    {
        if (PayloadDuringCountdown.TryGetValue(laneNumber, out JObject payload))
        {
            SocketCommunicator.Instance.SendStartGameCommand(payload);
        }
    }

    /// <summary>
    /// Stops the game for a given lane.
    /// </summary>
    /// <param name="laneNumber">The lane to stop (0 for Zone).</param>
    // FIXME: This method calls SendStartGameCommand, which seems incorrect. Review required.
    public void StopGame(int laneNumber)
    {
        if (laneNumber == 0)
        {
            if (PayloadDuringCountdown.ContainsKey(0))
            {
                SocketCommunicator.Instance.SendStartGameCommand(PayloadDuringCountdown[0]);
            }
        }
    }
    #endregion

    #region Helper Methods

    /// <summary>
    /// Processes game state changes for LANE mode.
    /// </summary>
    private void HandleLaneMode(JObject payload)
    {
        DemoManager.instance.ExitIdleVideoMode();
        currentGameMode = "LANE";
        Zone.instance.clearAllScreen();

        if (payload["gameState"] == null || payload["tag"] == null) return;

        string laneTag = payload["tag"].ToString();
        int laneNumber = int.Parse(laneTag.Substring(laneTag.Length - 1));
        Lane lane = LaneManager.instance.Lanes[laneNumber];
        string gameState = payload["gameState"].ToString();

        // Prevent redundant state processing
        if (CurrentGameState.TryGetValue(laneNumber, out string current) && current == gameState && (gameState == "GAME_RUNNING" || gameState == "GAME_COUNTDOWN"))
        {
            return;
        }

        CurrentGameState[laneNumber] = gameState;
        isLaneRunning[laneNumber] = (gameState == "GAME_COUNTDOWN" || gameState == "GAME_RUNNING");

        switch (gameState)
        {
            case "IDLE":
                DemoManager.instance.ExitDemoMode();
                AudioManager.Instance.StopAudio();
                lane.clearAllScreens();
                TargetManager.Instance.RemoveBackgroundEnemy(laneNumber);
                break;

            case "SELECT_PLAYER":
                DemoManager.instance.ExitDemoMode();
                AudioManager.Instance.StopAudio();
                lane.showCurrentPlayerScreen(payload);
                break;

            case "OBJECTIVE":
                DemoManager.instance.ExitDemoMode();
                AudioManager.Instance.StopAudio();
                int gameId = int.Parse(payload["objectivePayload"]["gameId"].ToString());
                lane.showGameReadyScreen(gameId);
                TargetManager.Instance.StartBackgroundEnemy(laneNumber, gameId);
                break;

            case "PLAYER_READY":
                AudioManager.Instance.StopAudio();
                lane.showGetReadyScreen();
                break;

            case "CHECK_GUN":
                AudioManager.Instance.StopAudio();
                lane.showCheckFirearmScreen();
                break;

            case "GAME_COUNTDOWN":
                AudioManager.Instance.StopAudio();
                PayloadDuringCountdown[laneNumber] = payload;
                lane.showGameCountDownScreen();
                break;

            case "GAME_RUNNING":
                AudioManager.Instance.StopAudio();
                int runningGameId = int.Parse(payload["objectivePayload"]["gameId"].ToString());
                gameTypesForLanes[laneNumber] = Games.games.Find(g => g.id == runningGameId).type;
                lane.showGameScreen(payload);
                lane.StartGameTimer();
                break;

            case "GAME_OVER":
            case "GAME_CANCELLED":
                HandleLaneGameOver(payload, lane, laneNumber, gameState);
                break;

            case "LEADERBOARD":
                AudioManager.Instance.StopAudio();
                lane.showLeaderboardScreen();
                TargetManager.Instance.RemoveBackgroundEnemy(laneNumber);
                break;

            case "SESSION_OVER":
                AudioManager.Instance.StopAudio();
                lane.showSessionOverScreen();
                break;
        }
    }

    /// <summary>
    /// Processes game state changes for ZONE or GAUNTLET mode.
    /// </summary>
    private void HandleZoneMode(JObject payload)
    {
        // Clear all individual lane screens
        foreach (var lane in LaneManager.instance.Lanes.Values)
        {
            lane.clearAllScreens();
        }

        currentGameMode = "ZONE";
        DemoManager.instance.ExitIdleVideoMode();

        string gameState = payload["gameState"].ToString();
        int zoneLaneNum = 0; // Zone mode uses index 0

        // Prevent redundant state processing
        if (CurrentGameState.TryGetValue(zoneLaneNum, out string current) && current == gameState && (gameState == "GAME_RUNNING" || gameState == "GAME_COUNTDOWN" || gameState == "GAME_OVER"))
        {
            return;
        }

        CurrentGameState[zoneLaneNum] = gameState;
        isLaneRunning[zoneLaneNum] = (gameState == "GAME_COUNTDOWN" || gameState == "GAME_RUNNING");

        Game game = null;
        if (payload["objectivePayload"]?["gameId"] != null)
        {
            game = Games.games.Find(g => g.id == int.Parse(payload["objectivePayload"]["gameId"].ToString()));
            currentZoneGame = game;
        }

        switch (gameState)
        {
            case "IDLE":
                DemoManager.instance.EnterIdleVideoMode();
                TargetManager.Instance.StopTargets();
                AudioManager.Instance.StopAllAudio();
                TargetManager.Instance.RemoveAllBackgroundEnemies();
                Zone.instance.clearAllScreen();
                break;

            case "OBJECTIVE":
                TargetManager.Instance.StopTargets();
                AudioManager.Instance.StopAudio();
                if (game != null) Zone.instance.showGameReadyScreen(game.id);
                break;

            case "SELECT_PLAYER":
                TargetManager.Instance.StopTargets();
                AudioManager.Instance.StopAudio();
                Zone.instance.showWelcomeScreen(payload);
                break;

            case "PLAYER_READY":
                TargetManager.Instance.StopTargets();
                AudioManager.Instance.PlayGetReady();
                Zone.instance.showGetReadyScreen();
                break;

            case "CHECK_GUN":
                TargetManager.Instance.StopTargets();
                Zone.instance.showCheckFirearmScreen();
                break;

            case "GAME_COUNTDOWN":
                AudioManager.Instance.PlayCountdown();
                AudioManager.Instance.PlayGame();
                PayloadDuringCountdown[zoneLaneNum] = payload;
                Zone.instance.showGameCountDownScreen();
                break;

            case "GAME_RUNNING":
                AudioManager.Instance.PlayStartShooting();
                if (game != null)
                {
                    gameTypesForLanes[zoneLaneNum] = game.type;
                    Zone.instance.showGameScreen(payload);
                    Zone.instance.StartGameTimer();
                }
                break;

            case "GAME_OVER":
            case "GAME_CANCELLED":
                HandleZoneGameOver(payload, game, gameState);
                break;

            case "LEADERBOARD":
                AudioManager.Instance.StopAudio();
                TargetManager.Instance.StopTargets();
                Zone.instance.showLeaderboardScreen();
                break;

            case "SESSION_OVER":
                TargetManager.Instance.StopTargets();
                Zone.instance.showSessionOverScreen();
                break;
        }
    }

    /// <summary>
    /// Handles the LANE_FINISHED command, typically to show the results for a single player.
    /// </summary>
    private void HandleLaneFinished(JObject payload)
    {
        string playerId = payload["data"]["playerId"].ToString();
        var participant = Zone.instance.GameScreenParticipantMetrics.Find(p => p.playerId == playerId);
        if (participant != null)
        {
            Debug.Log($"Lane Finished: Participant {playerId} found in lane {participant.laneNumber}");
            Zone.instance.showLaneFinished(participant.laneNumber);
        }
    }

    /// <summary>
    /// Handles the logic for GAME_OVER and GAME_CANCELLED states in Lane mode.
    /// </summary>
    private void HandleLaneGameOver(JObject payload, Lane lane, int laneNumber, string gameState)
    {
        AudioManager.Instance.StopAudio();
        Debug.Log($"Game Over for Lane {laneNumber}: {gameState}");
        TargetManager.Instance.StopLaneTargets(laneNumber);
        Debug.Log($"Stopping background enemy for Lane {laneNumber}");

        int gameId = int.Parse(payload["objectivePayload"]["gameId"].ToString());
        TargetManager.Instance.EndBackgroundEnemy(laneNumber, gameId);

        lane.StopGameTimer();
        lane.showGameOverScreen();
        lane.clearGameScoreEntries();
        lane.clearLeaderboardEntries();

        foreach (var pPlayer in lane.GameMetricsPlayersList)
        {
            Player player = Players.Instance.players.Find(p => p.PlayerTag == pPlayer.playerName.text);
            if (player != null) player.Score = "";
        }

        if (gameState == "GAME_OVER")
        {
            Game game = Games.games.Find(g => g.id == gameId);
            if (game == null) return;

            // Populate game results
            JArray gameResults = payload["gameResults"] as JArray;
            foreach (JToken result in gameResults)
            {
                Player player = Players.Instance.players.Find(p => p.PlayerID == result["playerId"].ToString());
                if (player == null) continue;

                string score = (game.type == GameDefinition.GameType.TIME_BASED)
                    ? FormatTimeScore(result["totalTime"].ToString())
                    : result["totalHits"].ToString();
                lane.AddGameScoreEntry(player.PlayerTag, "", score);
            }
            if (gameResults.Count > 0)
            {
                lane.SortGameboardList();
            }

            // Populate leaderboard
            JArray leaderboard = payload["leaderboard"] as JArray;
            int position = 1;
            foreach (JToken leader in leaderboard)
            {
                Player player = Players.Instance.players.Find(p => p.PlayerID == leader["playerId"].ToString());
                if (player == null) continue;

                string score = (game.type == GameDefinition.GameType.TIME_BASED)
                    ? FormatTimeScore(leader["totalTime"].ToString())
                    : leader["totalHits"].ToString();
                lane.AddLeaderboardEntry(player.PlayerTag, position.ToString(), score);
                position++;
            }
        }
    }

    /// <summary>
    /// Handles the logic for GAME_OVER and GAME_CANCELLED states in Zone mode.
    /// </summary>
    private void HandleZoneGameOver(JObject payload, Game game, string gameState)
    {
        AudioManager.Instance.StopAudio();
        AudioManager.Instance.PlayGameOver();
        TargetManager.Instance.StopTargets();

        Zone.instance.StopGameTimer();
        Zone.instance.showGameOverScreen();

        foreach (var pPlayer in Zone.instance.GameScreenParticipantMetrics)
        {
            Player player = Players.Instance.players.Find(p => p.PlayerTag == pPlayer.playerName.text);
            if (player != null) player.Score = "";
        }

        if (gameState == "GAME_OVER" && game != null)
        {
            UI_Manager.Instance.clearZoneLeaderboardItems();

            // Populate game results
            JArray gameResults = payload["gameResults"] as JArray;
            foreach (JToken result in gameResults)
            {
                Player player = Players.Instance.players.Find(p => p.PlayerID == result["playerId"].ToString());
                if (player == null) continue;

                string score = (game.type == GameDefinition.GameType.TIME_BASED)
                    ? FormatTimeScore(result["totalTime"].ToString())
                    : result["totalHits"].ToString();
                UI_Manager.Instance.addZoneCurrentGameScoreItem(player.PlayerTag, "", score);
            }
            UI_Manager.Instance.SortGameboardList();

            // Populate leaderboard
            JArray leaderboard = payload["leaderboard"] as JArray;
            int position = 1;
            foreach (JToken leader in leaderboard)
            {
                Player player = Players.Instance.players.Find(p => p.PlayerID == leader["playerId"].ToString());
                if (player == null) continue;

                string score = (game.type == GameDefinition.GameType.TIME_BASED)
                    ? FormatTimeScore(leader["totalTime"].ToString())
                    : leader["totalHits"].ToString();
                UI_Manager.Instance.addZoneLeaderboardItem(player.PlayerTag, position.ToString(), score);
                position++;
            }
        }
    }

    /// <summary>
    /// Checks for missing player data from a JArray and fetches it from the server.
    /// </summary>
    private void EnsurePlayersExist(JArray playerList, string idField)
    {
        if (playerList == null) return;

        var playerIdsToAdd = new HashSet<string>();
        foreach (JToken playerToken in playerList)
        {
            string playerId = playerToken[idField]?.ToString();
            if (!string.IsNullOrEmpty(playerId) && Players.Instance.players.Find(p => p.PlayerID == playerId) == null)
            {
                playerIdsToAdd.Add(playerId);
            }
        }

        if (playerIdsToAdd.Count > 0)
        {
            string url = $"{GlobalConfigs.Instance.GameControllerURL}:{GlobalConfigs.Instance.httpPort}/api/v1/players?player_ids[]=";
            url += string.Join("&player_ids[]=", playerIdsToAdd);
            StartCoroutine(HTTPCommunicator.Instance.GetPlayers(url));
        }
    }

    /// <summary>
    /// Updates a player's score on the UI after a target hit.
    /// </summary>
    private void UpdatePlayerScore(JObject payload, int laneNumber)
    {
        if (!gameTypesForLanes.ContainsKey(laneNumber) || gameTypesForLanes[laneNumber] != GameDefinition.GameType.HIT_BASED) return;

        Player player = Players.Instance.players.Find(p => p.PlayerID == payload["data"]["playerId"].ToString());
        if (player == null) return;

        int currentScore = 0;
        if (!string.IsNullOrEmpty(player.Score))
        {
            int.TryParse(player.Score, out currentScore);
        }
        player.Score = (currentScore + 1).ToString();

        // Update UI
        if (laneNumber == 0) // Zone mode
        {
            var participantUI = Zone.instance.GameScreenParticipantMetrics.Find(p => p.playerName.text == player.PlayerTag);
            if (participantUI != null)
            {
                participantUI.playerScore.text = player.Score;
            }
        }
        else // Lane mode
        {
            var participantUI = LaneManager.instance.Lanes[laneNumber].GameMetricsPlayersList.Find(p => p.playerName.text == player.PlayerTag);
            if (participantUI != null)
            {
                participantUI.playerScore.text = player.Score;
            }
        }
    }

    /// <summary>
    /// Checks if a player exists, and if not, triggers a full refresh.
    /// Note: This seems less efficient than EnsurePlayersExist and is currently not used.
    /// </summary>
    public void RefreshPlayers(JArray playerPayload)
    {
        foreach (JToken player in playerPayload)
        {
            Player playerInList = Players.Instance.GetPlayer(int.Parse(player["playerId"].ToString()));
            if (playerInList == null)
            {
                HTTPCommunicator.Instance.refreshPlayers();
                return;
            }
        }
    }

    /// <summary>
    /// Checks if any lane is running to control the camera cart.
    /// </summary>
    public void checkAndControlCart()
    {
        // bool isAnyLaneRunning = isLaneRunning.ContainsValue(true);
        // camCart.m_Speed = isAnyLaneRunning ? speed : 0;
    }

    /// <summary>
    /// Updates post-processing effects based on whether any game is running.
    /// </summary>
    private void UpdatePostProcessingEffects()
    {
        bool isAnyLaneRunning = isLaneRunning.Values.Any(isRunning => isRunning);

        if (isAnyLaneRunning)
        {
            if (!IsExposureReduced)
            {
                // StartCoroutine(SetExposure(exposureStartValue, exposureEndValue, 1));
                ToggleLights(false);
            }
        }
        else
        {
            if (IsExposureReduced)
            {
                // StartCoroutine(SetExposure(exposureEndValue, exposureStartValue, 1));
                ToggleLights(true);
            }
        }
    }

    /// <summary>
    /// Toggles the state of specified scene lights.
    /// </summary>
    /// <param name="state">True to enable, false to disable.</param>
    void ToggleLights(bool state)
    {
        foreach (Light light in LightsToToggle)
        {
            if (light != null)
            {
                light.gameObject.SetActive(state);
            }
        }
    }

    /// <summary>
    /// Formats a time score from milliseconds to a "mm:ss:fff" string.
    /// </summary>
    /// <param name="scoreInMillis">The score in milliseconds as a string.</param>
    /// <returns>Formatted time string.</returns>
    private string FormatTimeScore(string scoreInMillis)
    {
        if (int.TryParse(scoreInMillis, out int ms))
        {
            int seconds = ms / 1000;
            int milliseconds = ms % 1000;
            return $"{seconds:00}:{milliseconds:000}";
        }
        return "00:000";
    }
    #endregion
}
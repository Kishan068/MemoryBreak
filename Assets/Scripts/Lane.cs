using Mechanics;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lane : MonoBehaviour
{
    //A lane can contain multiple stations. The stations can be identified using the station's Id. 

    public int LaneId;

    public List<GameObject> laneStation = new List<GameObject>();
    //Create a dictionary to store the stations in the lane.
    public Dictionary<int, station> stationsMap = new Dictionary<int, station>();

    public GameObject WelcomeScreen;
    public List<ParticipatingPlayer> ParticipatingPlayersList = new List<ParticipatingPlayer>();

    public GameObject GameReadyScreen;

    public GameObject GameRunningScreen;

    public GameObject GetReadyScreen;

    public GameObject CheckFirearmScreen;


    public List<ParticipatingPlayer> GameMetricsPlayersList = new List<ParticipatingPlayer>();

    public Dictionary<int, GameObject> CurrentPlayer = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> GetReadyCurrentPlayer = new Dictionary<int, GameObject>();
    public GameObject GameCountDownScreen;
    public Dictionary<int, GameObject> GameScreen = new Dictionary<int, GameObject>();

    public GameObject GameOverScreen;

    public GameObject LeaderboardScreen;

    public GameObject SessionOverScreen;

    public TextMeshProUGUI GameTitle;
    public TextMeshProUGUI GameDescription;
    public CountUpTimer GameRunningTime;


    public GameObject GameScoreEntryPrefab;
    public GameObject GameScoreEntryParent;
    public List<GameObject> GameScoreEntries;

    public GameObject LeaderboardEntryPrefab;
    public GameObject LeaderboardEntryParent;
    public List<GameObject> LeaderboardEntries;

    int currentGameID;
    private void Start()
    {
        //Populate the dictionary with the stations in the lane.
        foreach (GameObject station in laneStation)
        {
            station stationScript = station.GetComponent<station>();
            stationsMap.Add(stationScript.stationId, stationScript);
            CurrentPlayer.Add(stationScript.stationId, stationScript.currentPlayer);
            GetReadyCurrentPlayer.Add(stationScript.stationId, stationScript.getReadyCurrentPlayer);
            GameScreen.Add(stationScript.stationId, stationScript.gameScreen);
        }
    }

    public void clearAllScreens()
    {
        WelcomeScreen.SetActive(false);
        GameReadyScreen.SetActive(false);
        foreach (KeyValuePair<int, GameObject> entry in CurrentPlayer)
        {
            entry.Value.SetActive(false);
        }
        foreach (KeyValuePair<int, GameObject> entry in GetReadyCurrentPlayer)
        {
            entry.Value.SetActive(false);
        }
        GameCountDownScreen.SetActive(false);

        foreach (KeyValuePair<int, GameObject> entry in GameScreen)
        {
            entry.Value.SetActive(false);
        }
        GameOverScreen.SetActive(false);

        foreach (ParticipatingPlayer player in ParticipatingPlayersList)
        {
            player.playerName.text = "Not Participating";
            player.playerImage.enabled = false;
        }
        GameRunningScreen.SetActive(false);
        GetReadyScreen.SetActive(false);
        CheckFirearmScreen.SetActive(false);
        LeaderboardScreen.SetActive(false);
        SessionOverScreen.SetActive(false);
    }

    public void showWelcomeScreen()
    {
        clearAllScreens();
        WelcomeScreen.SetActive(true);
    }

    public void showGameReadyScreen(int gameId)
    {
        clearAllScreens();
        Game game = Games.games.Find(x => x.id == gameId);
        currentGameID = gameId;
        GameTitle.text = game.title;
        Debug.Log("Game title is " + game.title);
        //GameDescription.text = game.description;
        //Based on the game id, and background ID in target manager, show the game description
        switch (TargetManager.Instance.backgroundID)
        {
            case 1:
                if(game.id == 1)
                {
                    GameDescription.text = "A giant enemy drone has invaded the skies, releasing swarms of smaller drones to do its dirty work. Shoot down the swarm before they overwhelm you.";
                }
                else if(game.id == 3)
                {
                    GameDescription.text = "A giant drone is flooding the sky with two waves of clustered enemy drones. Shoot them down as fast as possible—the quicker you clear the swarm, the better your chances. ";
                }
                else if(game.id == 4)
                {
                    GameDescription.text = "A quad-bot is sending enemy drones at you one by one, testing your reflexes and endurance. Take down as many as you can before you're overwhelmed. ";
                }
                else if (game.id == 5)
                {
                    GameDescription.text = "The mecha quad-bot is stepping up its assault, sending out enemies two at a time in a relentless rhythm. Hold your ground and destroy as many as you can.";
                }
                else if (game.id == 6)
                {
                    GameDescription.text = "The mecha quad-bot is unleashing enemies in groups of three, turning the battlefield into chaos. React fast, aim true, and take down as many as you can.";
                }
                break;
            case 2:
                if (game.id == 1)
                {
                    GameDescription.text = "A hulking swamp monster has emerged, summoning a pack of vicious spawn to hunt you down. Clear them out as fast as possible before they overwhelm you. ";
                }
                else if (game.id == 3)
                {
                    GameDescription.text = "The swamp monster rises, sending two waves of deadly spawn to tear through everything in their path. Wipe them out as quickly as you can—hesitation means defeat.";
                }
                else if (game.id == 4)
                {
                    GameDescription.text = "A monstrous creature erupts from the deep, thrashing its tentacles in every direction. Dodge the chaos and shoot down as many tentacle strikes as you can. ";
                }
                else if (game.id == 5)
                {
                    GameDescription.text = "The tentacle beast is on a rampage, lashing out with two tentacles at a time. Stay alert and shoot them down before they reach you. ";
                }
                else if (game.id == 6)
                {
                    GameDescription.text = "The tentacle monster grows more aggressive, striking with three tentacles at once. Blast them out of the air as fast as you can. ";
                }
                break;
            case 3:
                if (game.id == 1)
                {
                    GameDescription.text = "An evil pterodactyl swoops into the sky, unleashing a swarm of vicious minions to strike from above. Shoot them all down as fast as possible before they overwhelm you.";
                }
                else if (game.id == 3)
                {
                    GameDescription.text = "The evil pterodactyl circles overhead, sending two waves of deadly minions to dive-bomb your position. Take them out as fast as you can—hesitation means disaster.";
                }
                else if (game.id == 4)
                {
                    GameDescription.text = "A rampaging T-Rex has burst from the earth, hurling molten lava rocks at you one by one. Shoot down as many as you can before they hit. ";
                }
                else if (game.id == 5)
                {
                    GameDescription.text = "The T-Rex isn’t holding back—it's launching two blazing lava rocks at you with every stomp. Take them out fast before they crash down.";
                }
                else if (game.id == 6)
                {
                    GameDescription.text = "The T-Rex is going all out, kicking three searing lava rocks at you at once. Blast them mid-air before they rain destruction. ";
                }
                break;
            default:
                
                break;
        }
        GameReadyScreen.SetActive(true);
    }

    public void showGameOverScreen()
    {
           clearAllScreens();
        GameOverScreen.SetActive(true);
    }

    public void showGetReadyScreen()
    {
        clearAllScreens();
        GetReadyScreen.SetActive(true);
    }

    public void showCheckFirearmScreen()
    {
        clearAllScreens();
        CheckFirearmScreen.SetActive(true);
    }

    public void showLeaderboardScreen()
    {
        clearAllScreens();
        LeaderboardScreen.SetActive(true);
    }

    public void showSessionOverScreen()
    {
        clearAllScreens();
        SessionOverScreen.SetActive(true);

    }
    public void OnCountDownComplete()
    {
        //start the game
        GameEventHandler.Instance.StartGame(this.LaneId);
    }

    public void showCurrentPlayerScreen(JObject payload)
    {
        clearAllScreens();
        JArray playerPayloads = (JArray)payload["playerPayloads"];
        if (playerPayloads != null)
        {
            foreach (JObject playerPayload in playerPayloads)
            {
                //check if lanePayloads is not null
                JArray lanePayloads = (JArray)playerPayload["lanePayloads"];
                if (lanePayloads != null)
                {
                    foreach (JObject lanePayload in lanePayloads)
                    {
                        //Check if the laneId is the same as the current lane
                        int laneId = int.Parse(lanePayload["laneId"].ToString());
                        if (laneId == LaneId)
                        {
                            //check if activeStationIds is not null
                            JArray activeStationIds = (JArray)lanePayload["activeStationIds"];
                            if (activeStationIds != null)
                            {
                                foreach (JToken activeStationId in activeStationIds)
                                {
                                    ////check if the station is not already running
                                    //int stationNumber = int.Parse(activeStationId.ToString());
                                    //stationsMap[stationNumber].currentPlayerName.text = playerPayload["playerId"].ToString();
                                    //CurrentPlayer[stationNumber].SetActive(true);
                                    // Get the participating player whose stationNumber is same as the activeStationId
                                    ParticipatingPlayer participatingPlayer = ParticipatingPlayersList.Find(x => x.stationNumber == int.Parse(activeStationId.ToString()));
                                    if (participatingPlayer != null)
                                    {
                                        Debug.Log("Participating player  found " + playerPayload["playerId"].ToString());
                                        // Set the player name and image of the player in the participating player list, whose station number and lane number matches the active station id and lane id
                                        Player playerDetails = Players.Instance.GetPlayer(int.Parse(playerPayload["playerId"].ToString()));
                                        participatingPlayer.playerName.text = playerDetails.PlayerTag;
                                        participatingPlayer.playerId = playerDetails.PlayerID;
                                        participatingPlayer.playerImage.sprite = Players.Instance.players.Find(x => x.PlayerID == playerPayload["playerId"].ToString()).PlayerImage;
                                        participatingPlayer.playerImage.enabled = true;
                                    }
                                    else
                                    {
                                        Debug.Log("Participating player not found");
                                    }
                                    WelcomeScreen.SetActive(true);
                                }
                            }
                        }
                    }
                }
            }
        }
        HideNotParticipatingStations();
    }

    public void HideNotParticipatingStations()
    {
        foreach (ParticipatingPlayer player in ParticipatingPlayersList)
        {
            if (player.playerName.text == "Not Participating")
            {
                player.gameObject.SetActive(false);
            }
            else
            {
                player.gameObject.SetActive(true);
            }
        }
    }


    public void showPlayerReadyScreen(JObject payload)
    {
        clearAllScreens();
        JArray playerPayloads = (JArray)payload["playerPayloads"];
        if (playerPayloads != null)
        {
            foreach (JObject playerPayload in playerPayloads)
            {
                //check if lanePayloads is not null
                JArray lanePayloads = (JArray)playerPayload["lanePayloads"];
                if (lanePayloads != null)
                {
                    foreach (JObject lanePayload in lanePayloads)
                    {
                        //Check if the laneId is the same as the current lane
                        int laneId = int.Parse(lanePayload["laneId"].ToString());
                        if (laneId == LaneId)
                        {
                            //check if activeStationIds is not null
                            JArray activeStationIds = (JArray)lanePayload["activeStationIds"];
                            if (activeStationIds != null)
                            {
                                foreach (JToken activeStationId in activeStationIds)
                                {
                                    //check if the station is not already running
                                    int stationNumber = int.Parse(activeStationId.ToString());
                                    stationsMap[stationNumber].getReadyCurrentPlayerName.text = playerPayload["playerId"].ToString();
                                    GetReadyCurrentPlayer[stationNumber].SetActive(true);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void showGameCountDownScreen()
    {
        clearAllScreens();
        GameCountDownScreen.SetActive(true);
    }

    public void showGameScreen(JObject payload)
    {
        clearAllScreens();
        JArray playerPayloads = (JArray)payload["playerPayloads"];
        if (playerPayloads != null)
        {
            foreach (JObject playerPayload in playerPayloads)
            {
                //check if lanePayloads is not null
                JArray lanePayloads = (JArray)playerPayload["lanePayloads"];
                if (lanePayloads != null)
                {
                    foreach (JObject lanePayload in lanePayloads)
                    {
                        //Check if the laneId is the same as the current lane
                        int laneId = int.Parse(lanePayload["laneId"].ToString());
                        if (laneId == LaneId)
                        {
                            //check if activeStationIds is not null
                            JArray activeStationIds = (JArray)lanePayload["activeStationIds"];
                            if (activeStationIds != null)
                            {
                                foreach (JToken activeStationId in activeStationIds)
                                {
                                    //check if the station is not already running
                                  
                                    ParticipatingPlayer participatingPlayer = GameMetricsPlayersList.Find(x => x.stationNumber == int.Parse(activeStationId.ToString()));
                                    if (participatingPlayer != null)
                                    {
                                        // Set the player name and image of the player in the participating player list, whose station number and lane number matches the active station id and lane id
                                        Player playerDetails = Players.Instance.GetPlayer(int.Parse(playerPayload["playerId"].ToString()));
                                        participatingPlayer.playerName.text = playerDetails.PlayerTag;
                                        participatingPlayer.playerImage.sprite =  Players.Instance.players.Find(x => x.PlayerID == playerPayload["playerId"].ToString()).PlayerImage;
                                        participatingPlayer.playerImage.enabled = true;
                                        participatingPlayer.playerScore.text = "";
                                    }
                                    else
                                    {
                                        Debug.Log("Participating player not found");
                                    }
                                    GameRunningScreen.SetActive(true);
                                }
                            }
                        }
                    }
                }
            }
        }
        hideGameScreenParticipantMetrics();
    }

    public void hideGameScreenParticipantMetrics()
    {
        foreach (ParticipatingPlayer player in GameMetricsPlayersList)
        {
            if (player.playerName.text == "Not Assigned")
            {
                Debug.Log("Hiding not participating player");
                player.gameObject.SetActive(false);
            }
            else
            {
                Debug.Log("Displaying participating player " + player.playerName.text);
                player.gameObject.SetActive(true);
            }
        }

    }


    public void StartGameTimer()
    {
        Debug.Log("Timer is running");
        if (GameRunningTime != null)
        {
            if (GameRunningTime.timerIsRunning == false)
            {
                GameRunningTime.timerIsRunning = true;
            }
        }

    }

    public void StopGameTimer()
    {
        Debug.Log("Timer is not running");

        if (GameRunningTime != null)
        {
            if (GameRunningTime.timerIsRunning == true)
            {
                GameRunningTime.timerIsRunning = false;
            }
        }
    }

    
    public void clearGameScoreEntries()
    {
        Debug.Log("Cleared Gameboard");
        foreach (GameObject entry in GameScoreEntries)
        {
            Destroy(entry);
        }
        GameScoreEntries.Clear();
    }

    public void clearLeaderboardEntries()
    {
        Debug.Log("Cleared leaderboard");
        foreach (GameObject entry in LeaderboardEntries)
        {
            Destroy(entry);
        }
        LeaderboardEntries.Clear();
    }

    public void AddLeaderboardEntry(string playerTag, string position, string score)
    {
        GameObject leaderboardListItem = Instantiate(LeaderboardEntryPrefab, LeaderboardEntryParent.transform);
        Player player = Players.Instance.players.Find(x => x.PlayerTag == playerTag);
        if (player != null)
        {
            Debug.Log("Leaderboard Player found");
            leaderboardListItem.GetComponent<leaderboardItem>().SetLeaderboardItem(position, playerTag, score, Players.Instance.players.Find(x => x.PlayerID == player.PlayerID).PlayerImage);
            Debug.Log("Leaderboard Player added");
        }
        else
        {
            Debug.Log("Leaderboard Player not found");
            leaderboardListItem.GetComponent<leaderboardItem>().SetLeaderboardItem(position, playerTag, score, UserImageManager.Instance.GetPlayerImage(""));
        }


        LeaderboardEntries.Add(leaderboardListItem);
    }

    public void AddGameScoreEntry(string playerTag, string position, string score)
    {
        GameObject gameScoreListItem = Instantiate(GameScoreEntryPrefab, GameScoreEntryParent.transform);

        //get player from Players class whose playerTag matches the playerTag
        Player player = Players.Instance.players.Find(x => x.PlayerTag == playerTag);
        if (player != null)
        {Debug.Log("Gameboard Player found");
            gameScoreListItem.GetComponent<leaderboardItem>().SetLeaderboardItem(position, playerTag, score, Players.Instance.players.Find(x => x.PlayerID == player.PlayerID).PlayerImage);
            Debug.Log("Gameboard Player added");
        }
        else
        {
            Debug.Log("Gameboard Player not found");
            gameScoreListItem.GetComponent<leaderboardItem>().SetLeaderboardItem(position, playerTag, score, UserImageManager.Instance.GetPlayerImage(""));
            //zonecurrentGameListItem.GetComponent<leaderboardItem>().SetLeaderboardItem(position, playerTag, score, );
        }


        GameScoreEntries.Add(gameScoreListItem);
        
    }

    //sort game score entries by score
    public void SortGameboardList()
    {
        if (GameScoreEntries.Count == 0)
        {
            return;
        }
        Debug.Log("Sorting gameboard list");
        if (GameScoreEntries.Count == 0)
        {
            return;
        }
        //Score can either be in time format as ss:ms or in integer format. If score is in time format, then do ascending sort, else do descending sort
        if (GameScoreEntries[0].GetComponent<leaderboardItem>().playerScore.text.Contains(":"))
        {
            GameScoreEntries.Sort((x, y) => x.GetComponent<leaderboardItem>().playerScore.text.CompareTo(y.GetComponent<leaderboardItem>().playerScore.text));
        }
        else
        {
            GameScoreEntries.Sort((x, y) => y.GetComponent<leaderboardItem>().playerScore.text.CompareTo(x.GetComponent<leaderboardItem>().playerScore.text));
        }

        //GameBoardList.Sort((x, y) => y.GetComponent<LeaderboardListItem>().score.text.CompareTo(x.GetComponent<LeaderboardListItem>().score.text));

        //For every two items in the gameboardlist, increment the position number
        for (int i = 0; i < GameScoreEntries.Count; i++)
        {
            Debug.Log("Sorting gameboard list: adding position" + i);
            //if i+1 is even, set the position to i/2 + 1
            if ((i + 1) % 2 == 0)
            {
                GameScoreEntries[i].GetComponent<leaderboardItem>().playerPosition.text =  ((i / 2) + 1).ToString();
            }
            //if i+1 is odd, set the position to i/2 + 1
            else
            {
                GameScoreEntries[i].GetComponent<leaderboardItem>().playerPosition.text = ((i / 2) + 1).ToString();
            }

            // GameBoardList[i].GetComponent<LeaderboardListItem>().SetPosition(i + 1);
        }


        foreach (GameObject GameBoardListItem in GameScoreEntries)
        {
            GameBoardListItem.transform.SetAsLastSibling();
        }
    }

    public void updatePlayerImage(int PlayerId, Sprite playerImage)
    {
        //Find the player in Players whose id is playerId and update the image
        Player playerInAllPlayers  = Players.Instance.GetPlayer(PlayerId);
        
        foreach (ParticipatingPlayer player in ParticipatingPlayersList)
        {
            //Debug.Log("Player name is " + player.playerName.text + " and player tag is " + playerInAllPlayers.PlayerTag);
            if (player.playerName.text == playerInAllPlayers.PlayerTag)
            {
                player.playerImage.sprite = playerImage;
                
            }
        }
        foreach (ParticipatingPlayer player in GameMetricsPlayersList)
        {
            if (player.playerName.text == playerInAllPlayers.PlayerTag)
            {
                player.playerImage.sprite = playerImage;
               
            }
        }
        
    }

    public void updatePlayerTag(int PlayerId, string playerTag)
    {
        //Find the player in Players whose id is playerId and update the image
        Player playerInAllPlayers = Players.Instance.GetPlayer(PlayerId);


        foreach (ParticipatingPlayer player in ParticipatingPlayersList)
        {

            if (player.playerId == playerInAllPlayers.PlayerID)
            {
                player.playerName.text = playerTag;
            }
        }
        foreach (ParticipatingPlayer player in GameMetricsPlayersList)
        {
            if (player.playerId == playerInAllPlayers.PlayerID)
            {
                player.playerName.text = playerTag;
            }
        }
    }

}

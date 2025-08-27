using Mechanics;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Zone : MonoBehaviour
{
    //singleton
    public static Zone instance;

    //awake
    private void Awake()
    {
        instance = this;
    }


    public GameObject WelcomeScreen;
    public List<ParticipatingPlayer> ParticipatingPlayersList = new List<ParticipatingPlayer>();

    public GameObject GameReadyScreen;

    public GameObject GetReadyScreen;

    public GameObject CheckFirearmScreen;
   // public Dictionary<int, GameObject> GetReadyCurrentPlayer = new Dictionary<int, GameObject>();
    public GameObject GameCountDownScreen;

    public GameObject GameScreen;
    public List<ParticipatingPlayer> GameScreenParticipantMetrics = new List<ParticipatingPlayer>();
    public GameObject GameOverScreen;

    public GameObject LeaderboardScreen;

    public GameObject SessionOverScreen;

    public TextMeshProUGUI gameTitle;
    public TextMeshProUGUI gameDescription;

    public TextMeshProUGUI gameTitle2;
    public TextMeshProUGUI gameDescription2;

    public CountUpTimer GameRunningTime;

    public GameObject LeaderboardToDuplicate;
    private GameObject DuplicatedLeaderboard;

    public List<GameObject> LaneFinishedMessage = new List<GameObject>();
    public void clearAllScreen()
    {
      
        WelcomeScreen.SetActive(false);
        GameReadyScreen.SetActive(false);
        foreach (ParticipatingPlayer player in GameScreenParticipantMetrics)
        {
            //Debug.Log("Clearing screen " + player.gameObject.name);
            player.gameObject.SetActive(false);
        }
        GameCountDownScreen.SetActive(false);
        GameScreen.SetActive(false);
        GameOverScreen.SetActive(false);
        GetReadyScreen.SetActive(false);
        CheckFirearmScreen.SetActive(false);
        LeaderboardScreen.SetActive(false);
        SessionOverScreen.SetActive(false);
        foreach(GameObject laneFinishedMessage in LaneFinishedMessage)
        {
            laneFinishedMessage.SetActive(false);
        }
    }

    public void clearParticipatingPlayersUI()
    {
        foreach (ParticipatingPlayer player in ParticipatingPlayersList)
        { 
            player.playerName.text = "Not Participating";
            player.playerImage.enabled = false;
        }
    }

    public void clearGameMetrics()
    {
       
    }

    public void showWelcomeScreen(JObject payload)
    {
        clearAllScreen();
       
        clearParticipatingPlayersUI();

        int count = 0;
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
                       
                        //check if activeStationIds is not null
                        JArray activeStationIds = (JArray)lanePayload["activeStationIds"];
                            if (activeStationIds != null)
                            {
                                foreach (JToken activeStationId in activeStationIds)
                                {
                              
                                    //Set the player name and image of the player in the participating player list, whose station number and lane number matches the active station id and lane id
                                    int stationId = int.Parse(activeStationId.ToString());
                                    ParticipatingPlayer player = ParticipatingPlayersList.Find(x => x.stationNumber == stationId && x.laneNumber == laneId);
                                    if (player != null)
                                    {
                                       //Get player name from all Players
                                       Player playerDetails = Players.Instance.GetPlayer(int.Parse(playerPayload["playerId"].ToString()));
                                       player.playerName.text = playerDetails.PlayerTag;
                                        player.playerId = playerPayload["playerId"].ToString();
                                    //Set the image from the player in Players whose id is the same as the player id in the playerPayload

                                    player.playerImage.sprite = Players.Instance.players.Find(x => x.PlayerID == playerPayload["playerId"].ToString()).PlayerImage;
                                       player.playerImage.enabled = true;
                                    count++;
                                    }
                                    
                                }
                            }
                        
                    }
                }
                
            }
        }
        if(count == 0)
        {
            return;
        }
        HideNotParticipatingStations();
        WelcomeScreen.SetActive(true);
    }


    public void HideNotParticipatingStations()
    {         foreach (ParticipatingPlayer player in ParticipatingPlayersList)
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

    public void showGameReadyScreen(int gameId)
    {
        clearAllScreen();
        Game game = Games.games.Find(x => x.id == gameId);
        gameTitle.text = game.title;
        gameTitle2.text = game.title;
        gameDescription.text = game.description;
        gameDescription2.text = game.description;
        GameReadyScreen.SetActive(true);
    }

    public void showGameCountDownScreen()
    {
        clearAllScreen();
        GameCountDownScreen.SetActive(true);
    }


    public void showGameScreen(JObject payload)
    {
        clearAllScreen();
       


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
                        
                        //check if activeStationIds is not null
                        JArray activeStationIds = (JArray)lanePayload["activeStationIds"];
                        if (activeStationIds != null)
                        {
                            foreach (JToken activeStationId in activeStationIds)
                            {
                                
                                //Set the player name and image of the player in the participating player list, whose station number and lane number matches the active station id and lane id
                                int stationId = int.Parse(activeStationId.ToString());
                                ParticipatingPlayer player = GameScreenParticipantMetrics.Find(x => x.stationNumber == stationId && x.laneNumber == laneId);
                                if (player != null)
                                {
                                    Player playerDetails = Players.Instance.GetPlayer(int.Parse(playerPayload["playerId"].ToString()));
                                    player.playerName.text = playerDetails.PlayerTag;
                                    player.playerId = playerPayload["playerId"].ToString();
                                    player.gameObject.SetActive(true);
                                    player.playerScore.text = "";
                                    player.playerImage.sprite = Players.Instance.players.Find(x => x.PlayerID == playerPayload["playerId"].ToString()).PlayerImage;
                                    player.playerImage.enabled = true;
                                }

                            }
                        }

                    }
                }
            }
        }
        //hideGameScreenParticipantMetrics();
        GameScreen.SetActive(true);
    }

    

    public void hideGameScreenParticipantMetrics()
    {
        foreach (ParticipatingPlayer player in GameScreenParticipantMetrics)
        {
            if (player.playerName.text == "Not Assigned")
            {//Debug.Log("Hiding not participating player");  
                player.gameObject.SetActive(false);
            }
            else
            {
               // Debug.Log("Displaying participating player " + player.playerName.text);
                player.gameObject.SetActive(true);
            }
        }

    }

    public void showGameOverScreen()
    {
        clearAllScreen();
        GameOverScreen.SetActive(true);
    }

    public void showLeaderboardScreen()
    {
        clearAllScreen();
        LeaderboardScreen.SetActive(true);
        //Destroy the duplicated leaderboard if it exists
        if (DuplicatedLeaderboard != null)
        {
            Destroy(DuplicatedLeaderboard);
        }
        DuplicatedLeaderboard = Instantiate(LeaderboardToDuplicate, LeaderboardScreen.transform);
        //Move the duplicated leaderboard to the correct position
        DuplicatedLeaderboard.transform.localPosition = new Vector3(2000, 0, 0);
    }

    public void showGetReadyScreen()
    {
        clearAllScreen();
        GetReadyScreen.SetActive(true);
    }

    public void showCheckFirearmScreen()
    {
        clearAllScreen();
        CheckFirearmScreen.SetActive(true);
    }

    public void showSessionOverScreen()
    {
        clearAllScreen();
        SessionOverScreen.SetActive(true);
    }

    public void OnCountDownComplete()
    {
        //start the game
        GameEventHandler.Instance.StartGame(0);
    }

    public void StartGameTimer()
    {
       // Debug.Log("Timer is running");
        if(GameRunningTime != null )
        {
            if(GameRunningTime.timerIsRunning == false)
            {
                GameRunningTime.timerIsRunning = true;
            }
        }
       
    }
    
    public void StopGameTimer()
    {
        //Debug.Log("Timer is not running");
       
        if (GameRunningTime != null)
        {
            if (GameRunningTime.timerIsRunning == true)
            {
                GameRunningTime.timerIsRunning = false;
            }
        }
    }

    public void updatePlayerImage(int PlayerId, Sprite playerImage)
    {
        //Find the player in Players whose id is playerId and update the image
        Player playerInAllPlayers = Players.Instance.GetPlayer(PlayerId);

       
        foreach (ParticipatingPlayer player in ParticipatingPlayersList)
        {
            
            if (player.playerId == playerInAllPlayers.PlayerID)
            {
                player.playerImage.sprite = playerImage;
            }
        }
        foreach(ParticipatingPlayer player in GameScreenParticipantMetrics)
        {
            if( player.playerId == playerInAllPlayers.PlayerID)
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
        foreach (ParticipatingPlayer player in GameScreenParticipantMetrics)
        {
            if (player.playerId == playerInAllPlayers.PlayerID)
            {
                player.playerName.text = playerTag;
            }
        }
    }

    public void showLaneFinished(int laneNumber)
    {
        LaneFinishedMessage[laneNumber-1].SetActive(true);
       
    }





} 

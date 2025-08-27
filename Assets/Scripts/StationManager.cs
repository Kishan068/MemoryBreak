using Mechanics;
using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StationManager : MonoBehaviour
{
    // Public variables
    public GameObject WelcomeScreen;
    public GameObject GameReadyScreen;
    public GameObject PlayerReadyScreen;
    public GameObject GameCountDownScreen;
    public GameObject GameScreen;
    public GameObject GameOverScreen;

    public string mySessionTag = "";
    public GameSession myGameSession;
    public currentGameMetrics myGameMetrics;
    public GameObject VirtualStationPositionObject;
    public GameObject TargetLocationPrefab;
    public List<GameObject> targetLocationsList = new List<GameObject>();

    public Game currentGame;
    public int myStationNumber;
    public string myStationName;
    public int myLaneNumber;

    public GameObject StationLeaderboardListItem;
    public GameObject StationLeaderboardListContainer;
    public List<GameObject> StationLeaderboardListItems = new List<GameObject>();

    public GameObject StationCurrentGameListItem;
    public GameObject StationCurrentGameListContainer;
    public List<GameObject> StationCurrentGameListItems = new List<GameObject>();

    public List<Image> ImagesToSetStationColor = new List<Image>();
    //Hide all screens
    public void HideAllScreens()
    {
        //UI_Manager.Instance.hideZoneStartTimer();
        WelcomeScreen.SetActive(false);
        GameReadyScreen.SetActive(false);
        PlayerReadyScreen.SetActive(false);
        GameCountDownScreen.SetActive(false);
        GameScreen.SetActive(false);
        GameOverScreen.SetActive(false);
    }

    /*
     * Functions to show screens
     *     */
    public void ShowWelcomeScreen(JObject payload)
    {
        Debug.Log("Showing Welcome Screen");
        HideAllScreens();
        WelcomeScreen.SetActive(true);
        WelcomeScreen.GetComponent<MyStatus>().Status = "Welcome";
    }
    public void ShowGameReadyScreen(JObject payload)
    {
          HideAllScreens();
        GameReadyScreen.SetActive(true);
        //Find the game with the same gameId as the one in the payload
        Debug.Log("current Game is " + payload["objectivePayload"]["gameId"]);
        currentGame = Games.games.Find(x => x.id == (int)payload["objectivePayload"]["gameId"]);
        
        GameReadyScreen.GetComponent<MyStatus>().Status =currentGame.title;
    }
    public void ShowPlayerReadyScreen(JToken payload)
    {
        Debug.Log(payload.ToString());
        HideAllScreens();
        Debug.Log("setting player name" + payload["player"]["name"].ToString());
        PlayerReadyScreen.SetActive(true);
        myGameMetrics.score = 0;
        myGameSession.stationPlayer.PlayerName = payload["player"]["name"].ToString();
        myGameSession.stationPlayer.PlayerTag = payload["player"]["id"].ToString();
        //myGameSession.stationPlayer.PlayerImage = payload["data"]["player"]["photo"].ToString();
        PlayerReadyScreen.GetComponent<MyStatus>().Status = myGameSession.stationPlayer.PlayerTag;
    }
    public void ShowGameCountDownScreen(JObject payload)
    {
        HideAllScreens();
        myGameMetrics.gameTime = currentGame.timeout;
        myGameMetrics.myStationManager = this;
        myGameMetrics.currentInstructionNumber = 0;
        myGameMetrics.isFirstInstruction = true;
       
        GameCountDownScreen.GetComponent<GameStartCountdown>().mySessionTag = payload["tag"].ToString();
        JArray stationIds  = (JArray)payload["data"]["readyStations"];
        GameCountDownScreen.GetComponent<GameStartCountdown>().participatingStations = stationIds;
        GameCountDownScreen.SetActive(true);

    }
    public void ShowGameScreen()
    {
        
        HideAllScreens();
        GameScreen.SetActive(true);
        myGameMetrics.myStationManager = this;
        myGameMetrics.setPlayerTag(myGameSession.stationPlayer.PlayerTag);  
        myGameMetrics.setPlayerImage(UserImageManager.Instance.GetPlayerImage(myGameSession.stationPlayer.PlayerTag));
        myGameMetrics.UpdateGameTime (currentGame.timeout/1000);
        myGameMetrics.score = 0;
        myGameMetrics.ScoreText.text = "Score : 0";
        myGameMetrics.started = true;
        Debug.Log("Game time is " + currentGame.timeout);

        //GameScreen.GetComponent<MyStatus>().Status = "Now Playing: " + myGameSession.stationPlayer.PlayerName + "\n Score: 00" + "\n Time Remaining: 00";

    }
    public void ShowGameOverScreen()
    {
        HideAllScreens();
        GameOverScreen.SetActive(true);
        JObject jObject = new JObject();
        Debug.Log("Game Over");
        
        //SocketCommunicator.Instance.SendGameEvent(mySessionTag,"GAME_OVER", jObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Triggered");
        // Check if the collision object is a virtual target.
        VirtualTarget virtualTarget = other.GetComponent<VirtualTarget>();
        if (virtualTarget != null)
        {
            // Update the virtual target's player name to the station's name.
            //virtualTarget.SetPlayerName(gameObject.name);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Check if the collision object is a virtual target.
        VirtualTarget virtualTarget = other.GetComponent<VirtualTarget>();
        if (virtualTarget != null && virtualTarget.playerName == gameObject.name)
        {
            // Update the virtual target's player name to "0" (outside any station).
            //virtualTarget.SetPlayerName("0");
        }
    }

    public void setColliderSize()
    {
        GetComponent<BoxCollider2D>().size = new Vector2(912,2010);
    }

    public void endGame()
    {

    }

    public void createPredefinedTargetLocations(int numberOfLocations)
    {
        //Instantiate predefined locations 
        for (int i = 0; i < numberOfLocations; i++)
        {
            GameObject targetLocation = Instantiate(TargetLocationPrefab, VirtualStationPositionObject.transform.position, Quaternion.identity);
            targetLocation.transform.parent = transform;
            targetLocation.GetComponent<TargetAtLocation>().SetLocationNumber(i + 1);
            targetLocationsList.Add(targetLocation);
        }
    }

    public void hideUI()
    {
        //hide all target locations
        foreach (GameObject targetLocation in targetLocationsList)
        {
           //get the image component and disable it
            targetLocation.GetComponent<Image>().enabled = false;
            foreach (Transform child in targetLocation.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
        this.GetComponent<Image>().enabled = false;
    }

    public void showUI()
    {
        foreach (GameObject targetLocation in targetLocationsList)
        {
            //get the image component and disable it
            targetLocation.GetComponent<Image>().enabled = true;
            foreach (Transform child in targetLocation.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
        this.GetComponent<Image>().enabled = true;
    }


    public void addStationLeaderboardItem(int position, Player player, string type)
    {

        GameObject stationLeaderboardListItem = Instantiate(StationLeaderboardListItem, StationLeaderboardListContainer.transform);
        if(type == "HIT_BASED")
        {
            stationLeaderboardListItem.GetComponent<leaderboardItem>().SetLeaderboardItem(position.ToString(), player.PlayerTag, player.Score.ToString(), UserImageManager.Instance.GetPlayerImage(player.PlayerTag));

        }
        else if(type == "TIME_BASED")
        {
            string time = player.Score.ToString();
            //convert time from milli seconds to seconds
            float timeInSeconds = float.Parse(time) / 1000;
            //write time in format minutes:seconds:milliseconds
            string timeString = string.Format("{0}:{1}:{2}", Mathf.Floor(timeInSeconds / 60).ToString("00"), Mathf.Floor(timeInSeconds % 60).ToString("00"), ((timeInSeconds * 100) % 100).ToString("00"));
            stationLeaderboardListItem.GetComponent<leaderboardItem>().SetLeaderboardItem(position.ToString(), player.PlayerTag, timeString, UserImageManager.Instance.GetPlayerImage(player.PlayerTag));

        }
        
        StationLeaderboardListItems.Add(stationLeaderboardListItem);
       
    }

    public void clearStationLeaderboardItems()
    {
        foreach (GameObject item in StationLeaderboardListItems)
        {
            Destroy(item);
        }
        StationLeaderboardListItems.Clear();
        foreach (GameObject item in StationCurrentGameListItems)
        {
            Destroy(item);
        }
        StationCurrentGameListItems.Clear();
    }

    public void addStationCurrentGameItem(int position, Player player, string type)
    {

        GameObject stationCurrentGameListItem = Instantiate(StationCurrentGameListItem, StationCurrentGameListContainer.transform);
        if (type == "HIT_BASED")
        {
            stationCurrentGameListItem.GetComponent<leaderboardItem>().SetLeaderboardItem("", player.PlayerTag, player.Score.ToString(), UserImageManager.Instance.GetPlayerImage(player.PlayerTag));

        }
        else if (type == "TIME_BASED")
        {
            string time = player.Score.ToString();
            //convert time from milli seconds to seconds
            float timeInSeconds = float.Parse(time) / 1000;
            //write time in format minutes:seconds:milliseconds
            string timeString = string.Format("{0}:{1}:{2}", Mathf.Floor(timeInSeconds / 60).ToString("00"), Mathf.Floor(timeInSeconds % 60).ToString("00"), ((timeInSeconds * 100) % 100).ToString("00"));
            stationCurrentGameListItem.GetComponent<leaderboardItem>().SetLeaderboardItem(position.ToString(), player.PlayerTag, timeString, UserImageManager.Instance.GetPlayerImage(player.PlayerTag));

        }

        StationLeaderboardListItems.Add(stationCurrentGameListItem);

    }

    public void setAccentColors()
    {
        if(myStationNumber ==1 ) {
            //For every images in ImagesToSetStationColor, set its color to green
            foreach(Image image in ImagesToSetStationColor)
            {
                image.color = Color.green;
            }
        }
        else if(myStationNumber ==2 ) {
            foreach (Image image in ImagesToSetStationColor)
            {
                image.color = Color.red;
            }
        }
        else if(myStationNumber == 3) {
            foreach (Image image in ImagesToSetStationColor)
            {
                image.color = Color.blue;
            }
        }
        else if(myStationNumber==4)
        {
            foreach (Image image in ImagesToSetStationColor)
            {
                image.color = Color.yellow;
            }
        }
    }
}

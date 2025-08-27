
using TMPro;

using UnityEngine;
using UnityEngine.UI;

public class currentGameMetrics : MonoBehaviour
{
    //This class will be used to store the current game metrics
    //It will be used to send the metrics to the game controller
    //It will also be used to send the metrics to the game client

    public float timeElapsed;
    public float gameTime;
    public int score;
    public bool started;

    public TextMeshProUGUI PlayerNameText;
    public TextMeshProUGUI ScoreText;
    public TextMeshProUGUI TimeText;

    public Image playerImage;

    public StationManager myStationManager;

    public int currentInstructionNumber = 0;
    public int totalInstructions = 0;
    public int internalInstructionCycle = 0;
    public bool isFirstInstruction = true;

    public GameObject thisStationTimeRemainingObject;
    public GameObject ZoneTimeRemainingObject;
    
    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = 0;
        //gameTime = 0;
        score = 0;
        ScoreText.text = "Score : 0";

        
    }

    // Update game time
    public void UpdateGameTime(float time)
    {
        gameTime = time;
        //if (myStationManager.mySessionTag == "MULTI_STATION")
        //{
        //    thisStationTimeRemainingObject.SetActive(false);
        //    ZoneTimeRemainingObject = GameObject.FindGameObjectWithTag("TimeRemaining");
        //    if (ZoneTimeRemainingObject != null)
        //    {
        //        ZoneTimeRemainingObject.SetActive(true);
        //    }

        //}
        //else
        //{
        //    thisStationTimeRemainingObject.SetActive(true);
        //    ZoneTimeRemainingObject = GameObject.FindGameObjectWithTag("TimeRemaining");
        //    if (ZoneTimeRemainingObject != null)
        //    {
        //        ZoneTimeRemainingObject.SetActive(false);
        //    }

        //}
    }

    // Start the timer
    public void StartTimer()
    {
        timeElapsed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //If game is started
        if (started)
        {
            //if(isFirstInstruction)
            //{  //check if the InstructionsForGame from GameInstructions with current game Id has the first instruction.activationType as AUTO
            //    //if yes, then send the first instruction to the station
            //    totalInstructions = GameInstructions.instance.InstructionsForGame[myStationManager.currentGame.id].Count;
            //    if (GameInstructions.instance.InstructionsForGame[myStationManager.currentGame.id][0].activationType == "AUTO")
            //    {
            //        internalInstructionCycle = GameInstructions.instance.InstructionsForGame[myStationManager.currentGame.id][0].internalCycle;
            //        GameEngine.instance.TriggerInstruction(myStationManager.myStationNumber, myStationManager.currentGame.id, 0, GameInstructions.instance.InstructionsForGame[myStationManager.currentGame.id][0].selectionType);
            //        internalInstructionCycle--;
            //        if(internalInstructionCycle == 0)
            //        {
            //            currentInstructionNumber++;
            //        }
                   
                    
            //    }
            //    isFirstInstruction = false;
            //}

            

            gameTime  = gameTime - Time.deltaTime;
            
            //set the time text to the gameTime
            TimeText.text = "Time Remaining : " +  gameTime.ToString("F2");
            
            //if timeElapsed > gameTime, end the game
            if (gameTime <= 0)
            {
                //TO DO: end the game
                started = false;

                myStationManager.ShowGameOverScreen();
            }
        }
    }

    //trigger next instruction
    public void triggerNextInstruction()
    {
        //check if the currentInstructionNumber is less than the totalInstructions
        //if yes, then send the next instruction to the station

        if(internalInstructionCycle > 0)
        {
            GameEngine.instance.TriggerInstruction(myStationManager.myStationNumber, myStationManager.currentGame.id, currentInstructionNumber, GameInstructions.instance.InstructionsForGame[myStationManager.currentGame.id][currentInstructionNumber].selectionType);
            internalInstructionCycle--;
        }
        else
        {
            if (currentInstructionNumber < totalInstructions)
            {
                internalInstructionCycle = GameInstructions.instance.InstructionsForGame[myStationManager.currentGame.id][currentInstructionNumber].internalCycle;
                GameEngine.instance.TriggerInstruction(myStationManager.myStationNumber, myStationManager.currentGame.id, currentInstructionNumber, GameInstructions.instance.InstructionsForGame[myStationManager.currentGame.id][currentInstructionNumber].selectionType);
                currentInstructionNumber++;
            }
            else
            {
                //TO DO: end the game
                started = false;

                myStationManager.ShowGameOverScreen();
            }
        }
        

      //  GameEngine.instance.TriggerInstruction(myStationManager.myStationNumber, myStationManager.currentGame.id, currentInstructionNumber, GameInstructions.instance.InstructionsForGame[myStationManager.currentGame.id][currentInstructionNumber].selectionType);
    }

    //Set the player name
    public void setPlayerTag(string PlayerTag)
    {
        PlayerNameText.text = PlayerTag;
    }

    //update the score
    public void updateScore(int scoreToAdd)
    {
        score = score + scoreToAdd;
        ScoreText.text = "Score : " + score.ToString();
    }

    //return score
    public int getScore()
    {
        return score;
    }

    //set the player image
    public void setPlayerImage(Sprite playerImageSprite)
    {
        playerImage.sprite = playerImageSprite;
    }

}

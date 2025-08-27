
using TMPro;
using UnityEngine;

public class CountUpTimer : MonoBehaviour
{


    //on awake, set the singleton instance


    //start
    //private void Start()
    //{
    //    //set the flag to true to start the timer

    //    timerIsRunning = true;
    //}
    //public int desiredFixedUpdateRate = 20;
    private void OnEnable()
    {
        //Time.fixedDeltaTime = 1f / desiredFixedUpdateRate;
        // Set the maximum delta time to ensure maximum FPS
        //Time.maximumDeltaTime = Time.fixedDeltaTime;

        timeRemaining = 0;
    }
    //set the time for the countdown
    public float timeRemaining = 0;

    //Flag to start/stop the countdown
    public bool timerIsRunning;

    //Text to display the countdown
    public TextMeshProUGUI timerText;

    public TextMeshProUGUI timerText2;

    //Start the countdown timer when this is enabled and decrement it until it reaches zero with time.deltatime
    //then change the game state to game start
    //Set the flag to false to stop the timer
    private void Update()
    {
        if (timerIsRunning)
        {
            //Debug.Log("Timer is runningg");
            if (timeRemaining < 300)
            {
                //decrement the time remaining
                timeRemaining += Time.deltaTime;
            }
            else
            {
               // Debug.Log("Time endedd");
                //set the flag to false to stop the timer
                timerIsRunning = false;
            }
        }
        else
        {
           
        }
        //Display the time remaining with 0 decimal places
        DisplayTime(timeRemaining);
    }

    //Display the time remaining with 0 decimal places
    void DisplayTime(float timeToDisplay)
    {
        //convert the time to minutes and seconds and milliseconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = Mathf.FloorToInt((timeToDisplay * 1000) % 1000);

        //display the time in the timer text
        timerText.text = "Time : " +  string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);

        if(timerText2 != null)
        timerText2.text = "Time : " + string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
       
    }

}

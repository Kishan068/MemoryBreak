
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdown : MonoBehaviour
{

    //set the time for the countdown
    public float timeRemaining = 6;

    //Flag to start/stop the countdown
    public bool timerIsRunning = false;

    //Text to display the countdown
    public TextMeshProUGUI timerText;

    //Text to display the countdown
    public TextMeshProUGUI timerText2;

    //string to store the tag of the station requesting our names
    public string mySessionTag;

    public JArray participatingStations = new JArray();


    public Lane lane;

    public Zone zone;   

    //public AudioSource audioSource;

    bool canPlayAudio = false;
    //Start the countdown timer when this is enabled and decrement it until it reaches zero with time.deltatime
    //then change the game state to game start
    //Set the flag to false to stop the timer
    private void Update()
    {
        

        if (timerIsRunning)
        {


            if (timeRemaining > 1)
            {
                //decrement the time remaining
                timeRemaining -= Time.deltaTime;
            }
            else
            {
                if (lane != null)
                {
                    lane.OnCountDownComplete();

                }
                else if (zone != null)
                {
                    //start the game
                    zone.OnCountDownComplete();
                }
                //set the flag to false to stop the timer
                timerIsRunning = false;
            }

            
        }
        //Display the time remaining with 0 decimal places
        DisplayTime(timeRemaining);
    }

    //Display the time remaining with 0 decimal places
    void DisplayTime(float timeToDisplay)
    {
        //convert the time to minutes and seconds
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

       

        //Display only the seconds
        timerText.text = string.Format("{0}", seconds);

        if (timerText2 != null)
            timerText2.text = string.Format("{0}", seconds);
        //timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }


    //onenable set the flag to true to start the timer
    private void OnEnable()
    {
        timerIsRunning = true;
      
    }

    //ondisable set the flag to false to stop the timer
    private void OnDisable()
    {
        timerIsRunning = false;
        timeRemaining = 6;
    }

}

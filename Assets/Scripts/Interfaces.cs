using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interfaces : MonoBehaviour
{
    //Singleton
    public static Interfaces Instance;

    //Awake
    void Awake()
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

    //Interfaces in this reference are used to define common objects that can be used by multiple classes.

    public enum GameState { IDLE, OBJECTIVE, PLAYER_READY, GAME_COUNTDOWN, GAME_START, GAME_RUNNING, GAME_STOP, GAME_OVER };

    public enum GameType { TIME_BASED, HIT_BASED};

    public GameType gameType;
    public GameState gameState;


}

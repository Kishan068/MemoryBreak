using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScreenMode : MonoBehaviour
{
    //singleton
    public static GameScreenMode Instance;

    //Awake
    void Awake()
    {
        Instance = this;

        //DontDestroyOnLoad(gameObject);
    }

    public enum ScreenMode
    {
        Zone,
        Stations
    }

    public ScreenMode screenMode = ScreenMode.Stations; 
    
    public void SetScreenMode(ScreenMode mode)
    {
        screenMode = mode;
    }

    //get screen mode
    public ScreenMode GetScreenMode()
    {
        return screenMode;
    }

    
}

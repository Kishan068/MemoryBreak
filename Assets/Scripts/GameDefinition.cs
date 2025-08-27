using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class GameDefinition
{
    public enum GameType
    {
        TIME_BASED,
        HIT_BASED
    }

    public int id;
    public string title;
    public string image;
    public string description;
    public string goal;
    public string score;
    public string penalty;
    public int timeout;
    public GameType type;
    public string targetsRequired;
    public string instructions;
    public string category;
}

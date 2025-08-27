using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundEnemy : MonoBehaviour
{
    
    public int backgroundID;
    public int gameId;
    public int laneNumber;

    //get and set
    public int BackgroundID
    {
        get
        {
            return backgroundID;
        }
        set
        {
            backgroundID = value;
        }
    }

    public int GameId
    {
        get
        {
            return gameId;
        }
        set
        {
            gameId = value;
        }
    }

    public int LaneNumber
    {
        get
        {
            return laneNumber;
        }
        set
        {
            laneNumber = value;
        }
    }
}

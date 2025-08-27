using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mechanics;
public class GameSession 
{

    //The tag of the station that this session is for
    public string myStationTag;

    //The current state of the game for this station
    public Interfaces.GameState myGameState;
 
    // Current Zone Players
    public List<Player> zonePlayers = new List<Player>();

    // Current station Player
    public Player stationPlayer = new Player();
}

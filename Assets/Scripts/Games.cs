using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Games : MonoBehaviour
{
    // Available games in the Game Controller into a list
    [SerializeField]
    public static List<Game> games = new List<Game>();
    private Game currentGame;

    public static Games Instance;
    

    //singleton
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Games already exists!");
        }
    }

    //Expose the current game
    public Game CurrentGame { get => currentGame!=null?currentGame:new Game(); set => currentGame = value; }

    //Function to add games to the list
    public void AddGame(Game game)
    {
        games.Add(game);
    }

 
    //Function to set the current game to the game that was clicked
    public void SetCurrentGame(string name)
    {
        currentGame = games.Find(x => x.title == name);
        Debug.Log(currentGame.title + "is the current game");
    }


}

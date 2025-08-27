using Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leaderboardSort : MonoBehaviour
{
    //singleton
    public static leaderboardSort instance;

    //awake
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }


    public List<KeyValuePair<Player, int>> PlayerAndScores = new List<KeyValuePair<Player, int>>();

    public void AddPlayer(Player player, int score)
    {
        PlayerAndScores.Add(new KeyValuePair<Player, int>(player, score));
    }

    public void Sort()
    {
        PlayerAndScores.Sort((x, y) => y.Value.CompareTo(x.Value));
    }

    //Clears the list
    public void Clear()
    {
        PlayerAndScores.Clear();
    }
}

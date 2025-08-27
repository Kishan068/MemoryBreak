using Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    public List<Player> players = new List<Player>();

    public static Players Instance;

    //singleton
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Players already exists!");
        }
    }

    public void AddPlayer(Player player)
    {
        players.Add(player);
    }

    //Add a player
    public void AddPlayer(string playerId, string playerTag, string firstName, string lastName, string imageUrl)
    {
        //create a new player
        Player player = new Player();
        player.PlayerID = playerId;
        player.PlayerTag = playerTag;
        player.PlayerName = firstName + " " + lastName;
        player.ImageURL = imageUrl;

        players.Add(player);


    }

    public void RemovePlayer(Player player)
    {
        players.Remove(player);
    }

    public Player GetPlayer(int id)
    {
        foreach (Player player in players)
        {
            if (player.PlayerID == id.ToString())
            {
                return player;
            }
        }
        return null;
    }

    public bool IsPlayerAvailable(string playerId)
    {
        //find the player with the same id as the player id passed in
        Player player = players.Find(player => player.PlayerID == playerId);
        //check if the player exists
        if (player != null)
        {
            return true;
        }
        return false;
    }

    public bool IsPlayerImageAvailable(string playerId)
    {
        //find the player with the same id as the player id passed in
        Player player = players.Find(player => player.PlayerID == playerId);
        //check if the player has playerImage
        if (player != null)
        {
            if (player.PlayerImage == null)
            {
                //if the player does not have playerImage, get the player image from the server
                return false;
            }
            else
            {
                //if the player has playerImage, return true
                return true;
            }
        }
        return false;
    }

    public void UpdatePlayerImage(string playerId, Sprite sprite)
    {
        //find the player with the same id as the player id passed in
        Player player = players.Find(player => player.PlayerID == playerId);
        //check if the player exists
        if (player != null)
        {
            //update the player image
            player.PlayerImage = sprite;
        }

        foreach (KeyValuePair<int, Lane> lane in LaneManager.instance.Lanes)
        {
            lane.Value.updatePlayerImage(int.Parse(playerId), sprite);
        }

        Zone.instance.updatePlayerImage(int.Parse(playerId), sprite);
    }

    public void UpdatePlayer(string playerID, string playerTag, string firstName, string lastName, string imageUrl)
    {
        //find the player with the same player tag as the player tag passed in
        Player player = players.Find(player => player.PlayerID == playerID);
        //check if the player exists
        if (player != null)
        {
            player.PlayerName = firstName + " " + lastName;
            player.PlayerTag = playerTag;
            player.ImageURL = imageUrl;
        }

        foreach (KeyValuePair<int, Lane> lane in LaneManager.instance.Lanes)
        {
            lane.Value.updatePlayerTag(int.Parse(playerID), playerTag);
        }

        Zone.instance.updatePlayerTag(int.Parse(playerID), playerTag);
    }
}

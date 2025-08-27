using UnityEngine;
using UnityEngine.UI;

namespace Mechanics
{
    //Player class to store player data
    public class Player
    {
        // player data
        private string playerName;
        private string score;
        private int xp;
        private string playerID;
        private Sprite playerImage;
        private string playerTag;
        private string imageURL;

        // constructor
        public string PlayerName { get => playerName; set => playerName = value; }
        public string Score { get => score; set => score = value; }
        public int Xp { get => xp; set => xp = value; }
        public string PlayerID { get => playerID; set => playerID = value; }
        public Sprite PlayerImage { get => playerImage; set => playerImage = value; }
        public string PlayerTag { get => playerTag; set => playerTag = value; }

        // function to set the player image
        public string ImageURL { get => imageURL; set => imageURL = value; }

        // function to set the name of the player
        public void SetName(string name)
        {
            playerName = name;
        }

    }
}

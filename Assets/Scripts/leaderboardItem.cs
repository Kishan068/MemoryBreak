using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class leaderboardItem : MonoBehaviour
{
    public TextMeshProUGUI playerPosition;
    public TextMeshProUGUI playerName;
    public TextMeshProUGUI playerScore;
    public TextMeshProUGUI LaneName;
    public Image playerImage;

    // Set the leaderboard item's text
    public void SetLeaderboardItem(string position, string name, string score, Sprite PlayerImage)
    {
        playerPosition.text = position;
        playerName.text = name;
        playerScore.text = score;
        playerImage.sprite = PlayerImage;
    }

    public void SetLaneName(string laneName)
    {
        LaneName.text = laneName;
    }
    
}

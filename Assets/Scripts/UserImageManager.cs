using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserImageManager : MonoBehaviour
{
    //Singleton
    public static UserImageManager Instance;

    //awake
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("UserImageManager already exists!");
        }
    }


    //Dictionary to store the player's game tag and the image associated with it.
    //The images are stored in the Resources folder under UserImages folder
    public Dictionary<string, Sprite> userImages = new Dictionary<string, Sprite>();

    public Sprite playerImage;

    //Function to return the image associated with the player's game tag
    public Sprite GetPlayerImage(string imageUrl)
    {
        Sprite playerImage;
        if (userImages.TryGetValue(imageUrl, out playerImage))
        {
            return playerImage;
        }
        else
        {
            return null;
        }
    }



}

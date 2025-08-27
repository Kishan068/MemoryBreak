using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSettings : MonoBehaviour
{
    //singleton
    public static LoadSettings instance;

    public int stationsCount;

    public int targetsCount;



    //awake
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SaveGameSetting()
    {
        

        // Check if the input field is empty before saving.
        //if (!string.IsNullOrEmpty(newSetting))
        //{
        //    // Save the game setting using PlayerPrefs.
        //    PlayerPrefs.SetString(settingKey, newSetting);
        //    PlayerPrefs.Save();

        //    // Display a success message.
        //    saveStatusText.text = "Setting saved successfully!";
        //}
        //else
        //{
        //    // Display an error message if the input field is empty.
        //    saveStatusText.text = "Error: Setting cannot be empty!";
        //}
    }

    public void LoadGameSetting()
    {
        //// Load the game setting from PlayerPrefs.
        //string loadedSetting = PlayerPrefs.GetString(settingKey, "");

        //// Display the loaded game setting in the input field.
        //settingInputField.text = loadedSetting;

        //// You can use the loadedSetting value in your game to apply the loaded setting.
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMgr : MonoBehaviour
{
   
    public void LoadScene(int sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    private void Update()
    {
        //Switch to scene 2 if the player presses the space key
        if (Input.GetKeyDown(KeyCode.A))
        {
            LoadScene(0);
        }
        //Switch to scene 1 if the player presses the space key.
        if (Input.GetKeyDown(KeyCode.B))
        {
            LoadScene(1);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            LoadScene(2);
        }

    }

    public void UpdateBackground(int sceneNumber)
    {
        //Switch to scene 2 if the player presses the space key
        if (sceneNumber == 1)
        {
            LoadScene(0);
        }
        //Switch to scene 1 if the player presses the space key.
        if (sceneNumber == 2)
        {
            LoadScene(1);
        }
        if (sceneNumber == 3)
        {
            LoadScene(2);
        }
       
    }
}

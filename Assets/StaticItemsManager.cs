using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaticItemsManager : MonoBehaviour
{
    //Reset static items on scene unload
    void OnSceneUnload(Scene scene)
    {
       SocketCommunicator.Instance = null;
        UI_Manager.Instance = null;
        TargetManager.Instance = null;
    }

    void OnDestroy()
    {
        SceneManager.sceneUnloaded -= OnSceneUnload;
    }
}

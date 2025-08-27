
using UnityEngine;

public class GlobalConfigs : MonoBehaviour
{

    public string GameControllerURL = "http://localhost:3000";
    public string tag = "GameScreen";
    public string httpPort = "7500";
    public string socketIoPort = "7600";
    // Singleton
    public static GlobalConfigs Instance;


    public void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }

    }

    public void Start()
    {
        DontDestroyOnLoad(this);

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DemoManager : MonoBehaviour
{
    //singleton
    public static DemoManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        
    }

    public GameObject DemoPanel;

    public GameObject IdleVideo;
    public GameObject IdleVideoPanel;
    public void EnterDemoMode()
    {
        DemoPanel.SetActive(true);
       AudioManager.Instance.EnterDemoMode();
    }

    public void ExitDemoMode()
    {
        DemoPanel.SetActive(false);
        AudioManager.Instance.ExitDemoMode();
    }

    public void EnterIdleVideoMode()
    {
        IdleVideo.SetActive(true);
        IdleVideo.GetComponent<VideoPlayer>().Play();
        IdleVideoPanel.SetActive(true);
    }

    public void ExitIdleVideoMode()
    {
        IdleVideo.SetActive(false);
        IdleVideo.GetComponent<VideoPlayer>().Stop();
        IdleVideoPanel.SetActive(false);
    }
}

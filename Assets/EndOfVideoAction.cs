using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndOfVideoAction : MonoBehaviour
{
    private bool checkForEndOfVideo = false;
    //If the video player attached to this game object is done playing, do this action
    public void OnEndOfVideo()
    {
        //Do something
        DemoManager.instance.ExitIdleVideoMode();
    }

    //Find the end of video and call OnEndOfVideo
    private void Update()
    {
        if (checkForEndOfVideo)
        {
            if (!GetComponent<UnityEngine.Video.VideoPlayer>().isPlaying)
            {
                OnEndOfVideo();
                checkForEndOfVideo = false;
            }
        }

    }
}

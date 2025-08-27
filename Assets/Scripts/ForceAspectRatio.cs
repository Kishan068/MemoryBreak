using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceAspectRatio : MonoBehaviour
{
    public int targetWidth = 7680;  // Set your desired width
    public int targetHeight = 2600; // Set your desired height

    private void Start()
    {
        SetAspectRatio();
    }

    private void SetAspectRatio()
    {
        float targetAspect = (float)targetWidth / targetHeight;
        float windowAspect = (float)Screen.width / Screen.height;
        float scaleHeight = windowAspect / targetAspect;

        Camera camera = GetComponent<Camera>();

        //if (scaleHeight < 1.0f)
        //{
        //    Rect rect = camera.rect;
        //    rect.width = 1.0f;
        //    rect.height = scaleHeight;
        //    rect.x = 0;
        //    rect.y = (1.0f - scaleHeight) / 2.0f;
        //    camera.rect = rect;
        //}
        //else
        //{
        //    float scaleWidth = 1.0f / scaleHeight;
        //    Rect rect = camera.rect;
        //    rect.width = scaleWidth;
        //    rect.height = 1.0f;
        //    rect.x = (1.0f - scaleWidth) / 2.0f;
        //    rect.y = 0;
        //    camera.rect = rect;
        //}
        Rect rect = camera.rect;
        rect.height = scaleHeight;
        rect.y = (1.0f - scaleHeight) / 2.0f;
        camera.rect = rect;
    
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SetAspectRatio();
        }
    }
}


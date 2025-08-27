using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public Transform Target;

    public bool isTargetMainCamera = false;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Target);
        if (isTargetMainCamera)
        {
            //look at main camera
            transform.LookAt(Camera.main.transform);
        }
    }
}

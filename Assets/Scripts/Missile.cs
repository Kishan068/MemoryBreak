using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Transform SpawnTransform;
    public Transform TargetTransform;

    public float speed;

    private void OnEnable()
    {
        TargetTransform = transform.parent;
        transform.position = SpawnTransform.position;
        transform.rotation = SpawnTransform.rotation;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, TargetTransform.position, speed);
        Vector3 targetDir = Camera.main.transform.position - transform.position;
        //For every distance covered, rotate the missile towards the camera. The missile will look completely at the camera when it reaches it.
        float step = 10 * Time.deltaTime;

       
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);
    }
}

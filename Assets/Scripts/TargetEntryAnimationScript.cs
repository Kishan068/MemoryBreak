using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TargetEntryAnimationScript : MonoBehaviour
{
   
    public Transform target;
    public float speed;
    public float curveHeight;

    public Transform spawnTransform;
    public Vector3 targetPosition;
    public Vector3 targetRotation;

    public float timeToReachTarget = 0.5F;



    void Start()
    {
        //Find the gameobjects with the tag "Portal" and find whose name is "Portal 1"
        GameObject spawnPositionObjectReference = GameObject.FindGameObjectWithTag("OtherGamesSpawnLocation");
        //set the target position as the gameobject's parent's position
        targetPosition = this.transform.parent.position;
        targetRotation = this.transform.parent.rotation.eulerAngles;
        if(spawnPositionObjectReference != null)
        {
            spawnTransform = spawnPositionObjectReference.transform;
            //set the spawn position as the gameobject's parent's position
            spawnTransform.position = spawnPositionObjectReference.transform.position;
            spawnTransform.rotation = spawnPositionObjectReference.transform.rotation;
        }
        else
        {
            spawnTransform = this.transform.parent;
        }
        this.transform.position = spawnTransform.position;

    }

    private void OnDisable()
    {
        //targetPosition = this.transform.parent.position;
        //targetRotation = this.transform.parent.rotation.eulerAngles;
        //journeyLength = Vector3.Distance(spawnTransform.position, targetPosition);
            
        ////reset position and rotation
        //transform.position = spawnTransform.position;
        //transform.rotation = spawnTransform.rotation;

        ////reset startTime
        //startTime = Time.time;

    }
    void Update()
    {

        //Move the gameobject from the spawn position to the target position within TimeToReach seconds
        this.transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime / timeToReachTarget);
        this.transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), Time.deltaTime / timeToReachTarget);



    }

}

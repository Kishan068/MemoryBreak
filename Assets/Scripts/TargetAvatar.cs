using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetAvatar : MonoBehaviour
{
    public GameObject hitExplosion;
    public bool moveToTarget = false;
    GameObject targetLocationObject;
    public Vector3 targetLocation;
    public Animator myAnim;

    public void StartTarget()
    {
        //Instantiate an empty gameobject at the target location
        
        //set the parent of targetLocationObject to this object
     
        
       // moveToTarget = true;
       //look at the camera
       transform.LookAt(Camera.main.transform);
        myAnim = GetComponent<Animator>();
        //trigger the 'start' animation
       // myAnim.SetTrigger("Start");
        
    }

    public void StopTarget()
    {
        //stop animation
        Debug.Log("Stop Target");
        Instantiate(hitExplosion, transform.position, Quaternion.identity);
        myAnim.SetTrigger("miss");
        Destroy(gameObject);
    }

    public void HitTarget()
    {
        //instantiate hit explosion in this object
        Instantiate(hitExplosion, transform.position, Quaternion.identity);
        //hit animation
        Destroy(gameObject);
    }   

    //private void Update()
    //{
    //    //Move this gameobject to the target location. If it reaches the target location, then stop moving.
    //    // Move to the target position in 0.5 seconds.
    //    if (moveToTarget)
    //    {
    //        transform.position = Vector3.MoveTowards(transform.position, targetLocationObject.transform.position, 0.5f * Time.deltaTime);
    //        //If distance is less than 0.1f, then stop moving
    //        if (Vector3.Distance(transform.position, targetLocation) < 0.1f)
    //        {
    //            moveToTarget = false;
    //        }
    //    }
    //}
}

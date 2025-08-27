using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DroneTarget : MonoBehaviour
{
    //public Transform targetPosition; // The position to move the object to
    public float duration = 2.0f;  // The duration in seconds to move the object
    public float zAxisStoppingDistance = 10.0f; // The z-axis stopping distance
    private Vector3 startPosition; // The initial position of the object
    public GameObject Explosion;
    public GameObject meshObject;

    public bool PlayDeathSound = false;
    
    void Start()
    {
        
    }

    IEnumerator MoveOverTime(Vector3 targetPos, float duration)
    {
        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            // Check if the object is still valid
            if (this == null )
            {
                yield break; // Exit the coroutine if the object is destroyed
            }

            // Calculate the new position based on the elapsed time
            transform.position = Vector3.Lerp(startPosition, targetPos, elapsedTime / duration);
            meshObject.transform.LookAt(Camera.main.transform);
            // Increase the elapsed time by the time passed since the last frame
            elapsedTime += Time.deltaTime;

            // Wait for the next frame
            yield return null;
        }

        // Ensure the object is exactly at the target position at the end
        if (this != null)
        {
            transform.position = targetPos;
        }
    }

    public void StartDrone(Vector3 targetPostion)
    {
        this.gameObject.SetActive(true);
        // Store the initial position of the object
        startPosition = transform.position;
        Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3(targetPostion.x, targetPostion.y, zAxisStoppingDistance));
        //look at camera
       
        // Start the movement coroutine
        StartCoroutine(MoveOverTime(targetPos, duration));
    }

    public void StopDrone()
    {
       
        Destroy(gameObject, 0f);
    }
     
    public void HitDrone()
    {
        if (meshObject.GetComponent<Animator>())
        {
            meshObject.GetComponent<Animator>().enabled = false;
        }
        meshObject.GetComponent<Collider>().enabled = true;
        Explosion.SetActive(true);
        //Add a force in the direction of the main camera, with a little randomness
        meshObject.GetComponent<Rigidbody>().isKinematic =false;
        meshObject.GetComponent<Rigidbody>().useGravity = true;
        meshObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 400 + new Vector3(Random.Range(-400, 400), Random.Range(-100, 100), Random.Range(-400, 400)));

        if(PlayDeathSound)
        {
            GetComponent<AudioSource>().Play();
        }
        Destroy(gameObject, 10f);
    }


}

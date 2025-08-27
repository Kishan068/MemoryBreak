using System.Collections.Generic;
using UnityEngine;

public class StayAtPlace : MonoBehaviour
{
    public List<Transform> targets;
    public Transform target;
    public float speed;
    public float curveHeight;

    private Vector3 spawnPosition;
    private float journeyLength;
    private float startTime;

    public GameObject explosion;

    void Start()
    {
        spawnPosition = transform.position;
        GameObject[] TargetArray = GameObject.FindGameObjectsWithTag("targetWall");
        foreach (GameObject t in TargetArray)
        {
            targets.Add(t.transform);
        }
        //Get a random target
        target = targets[Random.Range(0, targets.Count)];
        journeyLength = Vector3.Distance(spawnPosition, target.position);
        startTime = Time.time;
    }

    void Update()
    {
        transform.LookAt(Camera.main.transform);
  

        //If we have reached the target
 
        if (Input.GetKeyDown(KeyCode.K))
        {
            //Kill animation
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
            }
            if (this.gameObject.GetComponent<Animator>())
            {
                this.gameObject.GetComponent<Animator>().SetTrigger("death");
            }

            Destroy(this.gameObject, 0f);
        }


    }
}
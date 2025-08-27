using System.Collections.Generic;
using UnityEngine;

public class CurveMovement : MonoBehaviour
{
    public List<Transform> targets;
    public Transform target;
    public float speed;
    public float curveHeight;

    public Transform spawnTransform;
    public Vector3 targetPosition;
    public Vector3 targetRotation;


    private float journeyLength;
    private float startTime;

    public GameObject explosion;

    void Start()
    {
        
        
        //set the target position as the gameobject's parent's position
        targetPosition = this.transform.parent.position;
        targetRotation = this.transform.parent.rotation.eulerAngles;

        
       
        journeyLength = Vector3.Distance(spawnTransform.position, targetPosition);
        startTime = Time.time;
        transform.position = spawnTransform.position;
        transform.rotation = spawnTransform.rotation;
    }

    private void OnDisable()
    {
        targetPosition = this.transform.parent.position;
        targetRotation = this.transform.parent.rotation.eulerAngles;
        journeyLength = Vector3.Distance(spawnTransform.position, targetPosition);

        //reset position and rotation
        transform.position = spawnTransform.position;
        transform.rotation = spawnTransform.rotation;

        //reset startTime
        startTime = Time.time;

    }
    void Update()
    {
       
        
        
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;

        Vector3 currentPos = Vector3.Lerp(spawnTransform.position, targetPosition, fracJourney);
        currentPos.y += Mathf.Sin(fracJourney * Mathf.PI) * curveHeight;

        transform.position = currentPos;


        //With ditance, rotate the missile to face the camera
        Vector3 targetDir = Camera.main.transform.position - transform.position;
        float step = fracJourney * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);


        //If we have reached the target
        if (fracJourney >= 1.0f)
        {
           
            curveHeight = 0;
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
           //Kill animation
           if(explosion != null)
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
            }
           if(this.gameObject.GetComponent<Animator>())
            {
                this.gameObject.GetComponent<Animator>().SetTrigger("death");
            }
          
            Destroy(this.gameObject, 0f);
        }

       
    }

}
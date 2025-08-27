using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class DroneCarrier : MonoBehaviour
{
    public Animator Animator;
    public GameObject dronePrefab; // Drone prefab
    public Transform[] orbitPositions; // Orbit positions around the carrier
    public Transform spawnPoint; // Spawn point for drones
    public float introOrbitSpeed = 50f; // Speed of drone orbit during intro animation

    private List<GameObject> activeDrones = new List<GameObject>(); // Active drones around the carrier
    private Queue<GameObject> standbyDrones = new Queue<GameObject>(); // Queue of standby drones

    private bool introComplete = false;

    public int activeDronesCount;

    public List<GameObject> DispatchedDrones = new List<GameObject>();

    public string targetIDToDestroy;

    public GameObject Explosion;

    public bool isFirstSetOfDrones = true;

    public float intervalToTurnOffFirstSetOfDrones = 5f;

    public bool lookAtPlayer;

    public float DispatchSpeed = 15f;

    public bool animateForEveryTarget = false;

    public bool SendBackDroneIfMissed = true;
      
    void Start()
    {
       //StartCoroutine(PerformIntroAnimation());
    }


    public void SpawnStandbyDrones()
    {
        StartCoroutine(PerformIntroAnimation());
    }

    private IEnumerator PerformIntroAnimation()
    {
        // Spawn and assign drones to orbit positions
        for (int i = 0; i < orbitPositions.Length; i++)
        {
            GameObject drone = Instantiate(dronePrefab, spawnPoint.position, Quaternion.identity);
            drone.SetActive(true);
            //drone.transform.position = orbitPositions[i].position;
            //make spawnPoint as parent
            drone.transform.SetParent(spawnPoint);
            activeDrones.Add(drone);
        }

        if(orbitPositions.Length>0)
        { 

        // Perform orbiting animation during intro
        float orbitTime = 3f; // Duration of the intro animation
        float elapsed = 0f;

        while (elapsed < orbitTime)
        {
            OrbitDrones();
            elapsed += Time.deltaTime;
            //move the drones towards orbitPositions[i].position
            for (int i = 0; i < activeDrones.Count; i++)
            {
                if (activeDrones[i] != null)
                {
                    activeDrones[i].transform.position = Vector3.MoveTowards(activeDrones[i].transform.position, orbitPositions[i].position, 1 * Time.deltaTime);
                }
            }

            yield return null;
        }
        }
        introComplete = true;

        yield return null;
    }

    private void OrbitDrones()
    {
        float orbitTime = 3f; // Duration of the intro animation
        float elapsed = 0f;

        while (elapsed < orbitTime)
        {
            
            elapsed += Time.deltaTime;
            //move the drones towards orbitPositions[i].position
            for (int i = 0; i < activeDrones.Count; i++)
            {
                if (activeDrones[i] != null)
                {
                    activeDrones[i].transform.position = Vector3.MoveTowards(activeDrones[i].transform.position, orbitPositions[i].position, 1 * Time.deltaTime);
                }
            }

        }

        for (int i = 0; i < activeDrones.Count; i++)
        {
            if (activeDrones[i] != null)
            { //make the drones move in wave motion up and down at the spawn point, each drone with a different phase. Do not rotate
                float waveSpeed = 1f;
                float waveHeight = 0f;
                float wavePhase = i * 0.5f;
                float waveOffset = Mathf.Sin(Time.time * waveSpeed + wavePhase) * waveHeight;
                activeDrones[i].transform.position = activeDrones[i].transform.position + new Vector3(0, waveOffset, 0);


            }
        }
    }
    private void Update()
    {
        //make the drones orbit around the carrier
        if (introComplete && orbitPositions.Length>0)
        {
            OrbitDrones();
        }

        //if the player presses the space key, send a drone to the target
        if (Input.GetKeyDown(KeyCode.Space))
        {
            OnGameStart("targetID", GameObject.Find("UI_Target").GetComponent<RectTransform>());
            //create a new drone after sending one to the target
            
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            Death();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            hitDrone("targetID");
        }
       
        if(lookAtPlayer)
        {
            transform.LookAt(Camera.main.transform);
        }
      
    }

    public void startTarget(string targetID, RectTransform uiTarget)
    {
        Debug.Log("Starting a new drone");
        OnGameStart(targetID, uiTarget);
    }

    private IEnumerator TurnOffFirstSetOfDrones()
    {
        Animator.ResetTrigger("Activate");
        Animator.SetTrigger("Activate");
        yield return new WaitForSeconds(intervalToTurnOffFirstSetOfDrones);
        
        isFirstSetOfDrones = false; 
    }

    public void OnGameStart(string targetID, RectTransform uiTarget)
    {
        Debug.Log("Starting a new drone1");
        //if (!introComplete || activeDrones.Count == 0) return;
        if (activeDrones.Count <=3)
        {
            Debug.Log("Starting a new drone2");
            QuickSpawnStandby();
        }

        // Convert UI target position to world space
        Vector3 worldTargetPosition = Camera.main.ScreenToWorldPoint(new Vector3(
            uiTarget.position.x, uiTarget.position.y, 10f));

        if (animateForEveryTarget)
        {
            Animator.ResetTrigger("Activate");
            Animator.SetTrigger("Activate");
        }

        Debug.Log("Starting a new drone");
        // Dispatch the first available drone
        //check if a drone is already dispatched

       

        GameObject dispatchedDrone = activeDrones[0];
        dispatchedDrone.GetComponent<TargetSeeker>().myTargetId = targetID;
        DispatchedDrones.Add(dispatchedDrone);
        activeDrones.RemoveAt(0);
        StartCoroutine(SendDroneToTarget(dispatchedDrone, worldTargetPosition));

        // Refill the spot with a standby drone if available
        if (standbyDrones.Count > 0)
        {
            GameObject newDrone = standbyDrones.Dequeue();
            newDrone.transform.position = orbitPositions[activeDrones.Count].position;
            activeDrones.Add(newDrone);
        }
        if(isFirstSetOfDrones)
        {

            StartCoroutine(TurnOffFirstSetOfDrones());
        }
        
        //AddStandbyDrone();
    }

    private IEnumerator SendDroneToTarget(GameObject drone, Vector3 targetPosition)
    {
        float travelSpeed = DispatchSpeed;
        drone.transform.SetParent(this.transform);
        if(isFirstSetOfDrones)
        {
            drone.GetComponent<Animator>().SetTrigger("ActivateFirstSet");
        }
        else
        {
            drone.GetComponent<Animator>().SetTrigger("Activate");
        }

        while (drone!=null && Vector3.Distance(drone.transform.position, targetPosition) > 0.1f)
        {
            drone.transform.position = Vector3.MoveTowards(drone.transform.position, targetPosition, travelSpeed * Time.deltaTime);

            yield return null;
        }

        // Optionally destroy the drone upon reaching the target
        //Destroy(drone);
    }
    public void missTarget(string targetID)
    {
        for (int i = 0; i < DispatchedDrones.Count; i++)
        {
            if (DispatchedDrones[i].GetComponent<TargetSeeker>().myTargetId == targetID)
            {
                //DispatchedDrones[i].GetComponent<TargetSeeker>().Kill();
                //Destroy(DispatchedDrones[i]);
                //DispatchedDrones.RemoveAt(i);
               if(SendBackDroneIfMissed)
                {
                    StartCoroutine(callBackDrone(DispatchedDrones[i]));
                }
                else
                {
                    DispatchedDrones[i].GetComponent<TargetSeeker>().KillWithoutAnimating();
                    DispatchedDrones.RemoveAt(i);
                }
                break;
              
            }
        }
    }

    public IEnumerator callBackDrone(GameObject drone)
    {
        //Make the drone travel to one of the active drone orbit positions
        float travelSpeed = 15f;


        //replace the active drone with this drone
        //GameObject previousActiveDrone = activeDrones[activeDrones.Count - 1];
        //activeDrones[activeDrones.Count - 1] = drone;
        //Destroy(previousActiveDrone);
        //Remove dispatched drone from DispatchedDrones
        DispatchedDrones.Remove(drone);

        while (drone != null && Vector3.Distance(drone.transform.position, orbitPositions[orbitPositions.Length-1].position) > 0.1f)
        {
            drone.transform.position = Vector3.MoveTowards(drone.transform.position, orbitPositions[orbitPositions.Length-1].position, travelSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(drone);   
        //make spawn point as parent
        //drone.transform.SetParent(spawnPoint);

    }

   



    public void hitDrone(string targetID)
    {
        //Find the drone in DispatchedDrones with the targetID in TargetSeeker and call kill function in it

        for (int i=0; i<DispatchedDrones.Count; i++)
        {
            if (DispatchedDrones[i].GetComponent<TargetSeeker>().myTargetId == targetID)
            {
                DispatchedDrones[i].GetComponent<TargetSeeker>().Kill();
                break;
            }
        }
        DispatchedDrones.RemoveAll(drones => drones.GetComponent<TargetSeeker>().myTargetId == targetID);

    }

    public void AddStandbyDrone()
    {
        //add a standby drone after .5 seconds
        if(activeDrones.Count ==0)
        {
            QuickSpawnStandby();
        }
        else
        {
            StartCoroutine(AddStandbyDroneAfterDelay());
        }

    }

    private void QuickSpawnStandby()
    {
        GameObject newDrone = Instantiate(dronePrefab, spawnPoint.position, Quaternion.identity);
        newDrone.SetActive(true);
        newDrone.transform.position = orbitPositions[activeDrones.Count].position;
        //make spawnPoint as parent
        newDrone.transform.SetParent(spawnPoint);
        activeDrones.Add(newDrone);
    }

    private IEnumerator AddStandbyDroneAfterDelay()
    {
        Debug.Log("Adding standby drone + Active count " + activeDrones.Count);
        yield return new WaitForSeconds(2f);
        //check if there is an null orbit position
        if (activeDrones.Count == orbitPositions.Length)
        {
            Debug.Log("No empty orbit position available");
            yield break;
        }
        else
        {
            //else fill the empty orbit position with a new drone
            GameObject newDrone = Instantiate(dronePrefab, spawnPoint.position, Quaternion.identity);
            newDrone.SetActive(true);
            newDrone.transform.position = orbitPositions[activeDrones.Count].position;
            //make spawnPoint as parent
            newDrone.transform.SetParent(spawnPoint);
            activeDrones.Add(newDrone);
            yield return null;
        }
 
    }

   

    public void Death()
    {
        Animator.SetTrigger("Death");
        GameObject explosion = Instantiate(Explosion, transform.position, Quaternion.identity);
        explosion.SetActive(true);
        //Remove all drones
        foreach (GameObject drone in activeDrones)
        {
            Destroy(drone);
        }

        //Remove all dispatched Drones
        foreach (GameObject drone in DispatchedDrones)
        {
            Destroy(drone);
        }
        //Remove all standby drones
        foreach (GameObject drone in standbyDrones)
        {
            Destroy(drone);
        }
        //Destroy(gameObject, 5f);
    }

    

}

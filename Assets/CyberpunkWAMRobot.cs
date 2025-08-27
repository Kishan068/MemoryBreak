using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberpunkWAMRobot : MonoBehaviour
{
    public List<GameObject> StandbyDrones;
    public GameObject newDroneObject;
    public GameObject newDroneSpawnLocation;

    public void SendDroneToAttack()
    {
        //if there are no drones available, create a new drone. 
        if (StandbyDrones.Count == 0)
        {
            GameObject newDrone = Instantiate(newDroneObject, newDroneSpawnLocation.transform.position, newDroneSpawnLocation.transform.rotation);
            StandbyDrones.Add(newDrone);
        }

        //send a random drone which is not null in list to attack,
        //if all drones are null, create a new drone.
        foreach (GameObject drone in StandbyDrones)
        {
            if (drone != null)
            {
                //drone.GetComponent<DroneController>().Attack();
                return;
            }
        }
        

    }
}

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEditor;
using TMPro;

[System.Serializable]
public class TargetData
{
    public string name;
    public Vector3 location;
}

public class TargetManager : MonoBehaviour
{
    public GameObject targetPrefab; // The prefab for the virtual target.
    public GameObject virtualTargetSpawnPrefab;
    public List<GameObject> virtualTargetSpawnList = new List<GameObject>();
    public List<GameObject> CreatedActualTargets = new List<GameObject>();

    public Dictionary<int,GameObject> virtualTargetDictionary = new Dictionary<int, GameObject>();
   
    //maintain a list of targets
    public List<GameObject> targetsList = new List<GameObject>();
    public GameObject targetsContainer;
    //singleton
    public static TargetManager Instance;

   



    public Dictionary<string, PhysicalTarget> targetDictionary = new Dictionary<string, PhysicalTarget>();


    public Transform targetSpawnLocation ;
    public List<DroneTarget> droneTargets = new List<DroneTarget>();
    public Dictionary<int, List<DroneTarget>> LaneTargetDictionary = new Dictionary<int, List<DroneTarget>>();

    public List<Transform> ParentSpawnAtLanePositions = new List<Transform>();
    public List<GameObject> ParentSpawns = new List<GameObject>();
    public int backgroundID;

    public List<GameObject> CyberCityBackgroundEnemyPrefabs = new List<GameObject>();

    

    //Awake
    void Awake()
    {
        Instance = this;

        virtualTargetDictionary.Clear();
        // for every virtual target spawn, add it to the dictionary
        int i = 1;
        foreach (GameObject virtualTargetSpawn in virtualTargetSpawnList)
        {
            virtualTargetDictionary.Add(i, virtualTargetSpawn);
            i++;
        }
        
    }

    private void Start()
    {
        //loadPhysicalTargetDictionary();
        //createTargetsFromGameObjects();
       
    }


    //function to start Background enemies
    public void StartBackgroundEnemy(int laneNumber, int gameId)
    {
        //Debug.Log("Starting a target" + targetName);
        GameObject enemy = CyberCityBackgroundEnemyPrefabs.Find(x => x.GetComponent<BackgroundEnemy>().gameId == gameId);
        //instantiate enemy at the spawn location
        if (enemy != null)
        {
            GameObject newEnemy = Instantiate(enemy, ParentSpawnAtLanePositions[laneNumber-1].position, Quaternion.identity);
            newEnemy.transform.parent = ParentSpawnAtLanePositions[laneNumber-1];
           
            newEnemy.GetComponent<BackgroundEnemy>().laneNumber = laneNumber;
            newEnemy.GetComponent<BackgroundEnemy>().gameId = gameId;

            //debug spawn locations (x,y,z of each newEnemy)


            //If the parent spawns list already has an object with the same lane number, remove it
            if (ParentSpawns.Exists(x => x.GetComponent<BackgroundEnemy>().laneNumber == laneNumber))
            {
                GameObject enemyToRemove = ParentSpawns.Find(x => x.GetComponent<BackgroundEnemy>().laneNumber == laneNumber);
                ParentSpawns.Remove(enemyToRemove);
                Destroy(enemyToRemove);
            }
            ParentSpawns.Add(newEnemy);

        }
    }

    public void EndBackgroundEnemy(int laneNumber, int gameId)
    {
        //In Parentspawns, find the object in which the gameID of BackgroundEnemy component is equal to gameId
        Debug.Log("Finding enemy to end" + gameId);
        GameObject enemy = ParentSpawns.Find(x => x.GetComponent<BackgroundEnemy>().laneNumber == laneNumber);
        Debug.Log("Found enemy to end" + enemy);
        //ParentSpawns.Remove(enemy);
        if (enemy != null)
        {
            enemy.GetComponent<DroneCarrier>().Death();
        }
       
    }

    public void RemoveBackgroundEnemy(int laneNumber)
    {
        //In Parentspawns, find the object in which the gameID of BackgroundEnemy component is equal to gameId
        GameObject enemy = ParentSpawns.Find(x => x.GetComponent<BackgroundEnemy>().laneNumber == laneNumber);
        if (enemy != null)
        {
            ParentSpawns.Remove(enemy);
            Destroy(enemy);
        }
     
    }
  
    public void RemoveAllBackgroundEnemies()
    {
        foreach(GameObject enemy in ParentSpawns)
        {
            Destroy(enemy);
        }
        ParentSpawns.Clear();
    }


    //function to start specific target
    public void StartTarget(string targetName, int laneNumber)
    {
       // Debug.Log("Starting a target" + targetName);

       GameObject target = targetsList.Find(x => x.GetComponent<VirtualTarget>().targetName == targetName);
        Debug.Log("Found target " + target);
        //Add random offset to the target spawn location
        Vector3 vector3 = targetSpawnLocation.position + new Vector3(Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f), Random.Range(-3.0f, 3.0f));
        
        if (target!=null)
        {
            
            target.GetComponent<VirtualTarget>().StartTarget(vector3, virtualTargetSpawnPrefab);
            target.GetComponent<VirtualTarget>().laneNumber = laneNumber;
        }

    }



    public void StartTargetNew(int laneNumber, string targetName)
    {
        // Debug.Log("Starting a target" + targetName);
        GameObject target = targetsList.Find(x => x.GetComponent<VirtualTarget>().targetName == targetName);
       

        //if (target != null)
        //{
        //    WAM.GetComponent<DroneCarrier>().startTarget(targetName, target.GetComponent<RectTransform>());
        //}
        if (target!=null)
        {
            //Find the child of the item in ParentSpawnAtLanePositions where laneNumber is the index-1  
            GameObject EnemyAtLane = ParentSpawnAtLanePositions[laneNumber-1].GetChild(0).gameObject;
            EnemyAtLane.GetComponent<DroneCarrier>().startTarget(targetName, target.GetComponent<RectTransform>());

        }

    }



    //function to stop targets
    public void StopTargets()
    {
        //Debug.Log("Stopping all targets");
        foreach (GameObject target in targetsList)
        {
            //Debug.Log("Trying to stop Target " + target);
            target.GetComponent<VirtualTarget>().StopTarget();
        }

        //foreach(PhysicalTarget target in physicalTargetsInGame)
        //{
        //    target.StopTarget();
        //}

        //stop all drone targets


    }

    public void StopLaneTargets(int LaneNumber)
    {
        foreach (GameObject target in targetsList)
        {
           if(target.GetComponent<VirtualTarget>().laneNumber == LaneNumber)
            {
                target.GetComponent<VirtualTarget>().StopTarget();
            }
        }

    }

   

    //function to hit targets
    public void HitTarget(string targetName)
    {
        Debug.Log("Hitting  a targetx");
        GameObject target = targetsList.Find(x => x.GetComponent<VirtualTarget>().targetName == targetName);
        //Shoot tracer
        //VirtualStationManager.instance.CreatedVirtualStations[TargetMapping.instance.StationTargetPairs[targetName]-1].GetComponent<myShooter>().Shoot(target.GetComponent<VirtualTarget>().myTagetAvatar);


        
        target.GetComponent<VirtualTarget>().HitTarget();

        if (targetDictionary.ContainsKey(targetName))
        {
            targetDictionary[targetName].HitTarget();
        }

    }

    public void HitTargetNew(int laneNumber, string targetName)
    {
        GameObject target = targetsList.Find(x => x.GetComponent<VirtualTarget>().targetName == targetName);
        if (target != null)
        {
            Debug.Log("Stopping  a targetx");
            //Find the child of the item in ParentSpawnAtLanePositions where laneNumber is the index-1  
            GameObject EnemyAtLane = ParentSpawnAtLanePositions[laneNumber - 1].GetChild(0).gameObject;
            EnemyAtLane.GetComponent<DroneCarrier>().hitDrone(targetName);
            Debug.Log("Stopped  a targetx");
        }
    }

    public void StopTarget(string targetName)
    {
        Debug.Log("Stopping  a targetx");
        GameObject target = targetsList.Find(x => x.GetComponent<VirtualTarget>().targetName == targetName);
        target.GetComponent<VirtualTarget>().StopTarget();
        if (targetDictionary.ContainsKey(targetName))
        {
            targetDictionary[targetName].StopTarget();
        }
    }
    public void StopTargetNew(int laneNumber, string targetName)
    {
        // Debug.Log("Starting a target" + targetName);
        GameObject target = targetsList.Find(x => x.GetComponent<VirtualTarget>().targetName == targetName);


        //if (target != null)
        //{
        //    WAM.GetComponent<DroneCarrier>().missTarget(targetName);
        //}

        if (target != null)
        {
            //Find the child of the item in ParentSpawnAtLanePositions where laneNumber is the index-1  
            GameObject EnemyAtLane = ParentSpawnAtLanePositions[laneNumber - 1].GetChild(0).gameObject;
            EnemyAtLane.GetComponent<DroneCarrier>().missTarget(targetName);

        }
    }


    public void hideTargets()
    {
        // set all targets to inactive
        foreach (GameObject target in targetsList)
        {
            //get the image component and make alpha 0
            target.GetComponent<Image>().color = new Color(1, 1, 1, 0.2f);
            //hide all children of this target
            foreach (Transform child in target.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
    
    //show targets
    public void showTargets()
    {
        // set all targets to active
        foreach (GameObject target in targetsList)
        {
            target.GetComponent<Image>().color = new Color(1, 1, 1, 1);
            foreach (Transform child in target.transform)
            {
                child.gameObject.SetActive(true);
            }
        }
    }

    public void loadPhysicalTargetDictionary()
    {
        targetDictionary.Clear();
       
    }
  
    public void spawnTargetsForTesting()
    {
        GameObject target = targetsList.Find(x => x.GetComponent<VirtualTarget>().targetName == "84:32:75:38:bc:d8-SS");

        if (target != null)
        {
            target.GetComponent<VirtualTarget>().StartTarget(targetSpawnLocation.position, virtualTargetSpawnPrefab);
            target.GetComponent<VirtualTarget>().laneNumber = 1;
        }
    }

    public void spawnAllTargetsForTesting()
    {
        foreach (GameObject target in targetsList)
        {
            target.GetComponent<VirtualTarget>().StartTarget(targetSpawnLocation.position, virtualTargetSpawnPrefab);
            target.GetComponent<VirtualTarget>().laneNumber = 1;
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.X))
        {
            spawnAllTargetsForTesting();
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            foreach (GameObject target in targetsList)
            {
                HitTarget(target.GetComponent<VirtualTarget>().targetName);
            }
                
        }
    }


    //save the positions of all objects in targetsList to playerprefs
    public void saveTargets()
    {
        //save the number of targets
        PlayerPrefs.SetInt("numTargets", targetsList.Count);
        //save the positions of all targets
        for (int i = 0; i < targetsList.Count; i++)
        {
            PlayerPrefs.SetFloat("target" + i + "x", targetsList[i].transform.position.x);
            PlayerPrefs.SetFloat("target" + i + "y", targetsList[i].transform.position.y);
            PlayerPrefs.SetFloat("target" + i + "z", targetsList[i].transform.position.z);
        }
    }

    //When load is called, move the targets to the positions saved in playerprefs
    public void loadTargets()
    {
        //get the number of targets
        int numTargets = PlayerPrefs.GetInt("numTargets");
        //get the positions of all targets
        for (int i = 0; i < numTargets; i++)
        {
            float x = PlayerPrefs.GetFloat("target" + i + "x");
            float y = PlayerPrefs.GetFloat("target" + i + "y");
            float z = PlayerPrefs.GetFloat("target" + i + "z");
            targetsList[i].transform.position = new Vector3(x, y, z);
        }
    }

   
}

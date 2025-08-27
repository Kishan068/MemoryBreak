using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualStationManager : MonoBehaviour
{

    //singleton
    public static VirtualStationManager instance;

    public List<int> stationNumber = new List<int>();
    public List<GameObject> CreatedVirtualStations = new List<GameObject>();
    public Dictionary<int, GameObject> Dict_CreatedVirtualGameBases = new Dictionary<int, GameObject>();
    public List<int> gameId = new List<int>();
    public List<GameObject> VirtualDemoGameBases = new List<GameObject>();
    public GameObject CreatedGameBases ;

    //awake
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public GameObject virtualStationPrefab;

    public void CreateVirtualStation(Vector3 spawnPosition, int stationNumber)
    {
        GameObject virtualStation = Instantiate(virtualStationPrefab, Camera.main.transform);
        spawnPosition.y += 0.1f;
        virtualStation.transform.position = spawnPosition;
        //virtualStation.transform.parent = transform;
        CreatedVirtualStations.Add(virtualStation);
    }

    public void createVirtualGameBase(int gameId, int stationNumber)
    {
        //check if gamebase already exists in Dict_CreatedVirtualGameBases
        if (Dict_CreatedVirtualGameBases.ContainsKey(stationNumber))
        {
            //if yes, then destroy the gamebase
            Destroy(Dict_CreatedVirtualGameBases[stationNumber]);
            Dict_CreatedVirtualGameBases.Remove(stationNumber);
        }
   
        //create gamebase
        //GameObject virtualGameBase = Instantiate(VirtualDemoGameBases[gameId-1], CreatedVirtualStations[stationNumber-1].transform.position, Quaternion.identity);
        Vector3 worldPosition = new Vector3(0, 0, 0);
        Ray ray = Camera.main.ScreenPointToRay(UI_Manager.Instance.stationsList[stationNumber-1].GetComponent<StationManager>().VirtualStationPositionObject.GetComponent<RectTransform>().position);
        
        worldPosition = ray.GetPoint(5f);

        //worldPosition.y = 03f;
        //GameObject virtualGameBase = Instantiate(VirtualDemoGameBases[gameId - 1], CreatedVirtualStations[stationNumber - 1].transform.position, Quaternion.identity);
        GameObject virtualGameBase = Instantiate(VirtualDemoGameBases[gameId - 1], worldPosition, Quaternion.identity);
        virtualGameBase.transform.parent = CreatedVirtualStations[stationNumber - 1].transform;
        virtualGameBase.transform.LookAt(Camera.main.transform);    
        Vector3 newPosition = new Vector3(virtualGameBase.transform.localPosition.x, virtualGameBase.transform.localPosition.y+10, virtualGameBase.transform.localPosition.z);
        //Vector3 newPosition = new Vector3(0, 1f, 15f);
        virtualGameBase.transform.localPosition = newPosition;
            CreatedGameBases =  virtualGameBase;
        Dict_CreatedVirtualGameBases.Add(stationNumber, virtualGameBase);
    }
    
    public void clearVirtualGameBase(int stationNumber)
    {
        if (Dict_CreatedVirtualGameBases.ContainsKey(stationNumber))
        {
            //if yes, then destroy the gamebase
            Destroy(Dict_CreatedVirtualGameBases[stationNumber]);
            Dict_CreatedVirtualGameBases.Remove(stationNumber);
        }
    }

}

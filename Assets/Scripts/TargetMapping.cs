using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TargetMapping : MonoBehaviour
{
    //singleton
    public static TargetMapping instance;
    public JObject targetMappingdata;
    public List<StationTarget> stationTargets = new List<StationTarget>();
    public Dictionary<string,int> StationTargetPairs = new Dictionary<string, int>();
    //awake
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //Dictionary to save the target mapping against each station.
    //public Dictionary<int, StationTargetMapping> targetMapping = new Dictionary<int, StationTargetMapping>();


    //create a dictionary to save the target mapping. Example payload is as below.

    /* {"zone1":{"stations":[{"stationId":1,"targets":[{"deviceId":1,"elementId:"STEEL_1",locationX":0,"locationY":0,"locationZ":0},
     * {"deviceId":2,"elementId:"STEEL_2","locationX":1,"locationY":0,"locationZ":0},
     * {"deviceId":3,"elementId:"STEEL_3","locationX":2,"locationY":0,"locationZ":0},
     * {"deviceId":4,"elementId:"STEEL_4","locationX":3,"locationY":0,"locationZ":0}]},
     * {"stationId":2,"targets":[{"deviceId":5,"elementId:"STEEL_5","locationX":0,"locationY":0,"locationZ":0},
     * {"deviceId":6,"elementId:"STEEL_6","locationX":1,"locationY":0,"locationZ":0},
     * {"deviceId":7,"elementId:"STEEL_7","locationX":2,"locationY":0,"locationZ":0},
     * {"deviceId":8,"elementId:"STEEL_8","locationX":3,"locationY":0,"locationZ":0}]},
     * {"stationId":3,"targets":[{"deviceId":9,"elementId:"STEEL_1","locationX":0,"locationY":0,"locationZ":0},
     * {"deviceId":10,"elementId:"STEEL_1","locationX":1,"locationY":0,"locationZ":0},
     * {"deviceId":11,"elementId:"STEEL_1","locationX":2,"locationY":0,"locationZ":0},
     * {"deviceId":12,"elementId:"STEEL_1","locationX":3,"locationY":0,"locationZ":0}]},
     * {"stationId":4,"targets":[{"deviceId":13,"elementId:"STEEL_1","locationX":0,"locationY":0,"locationZ":0},
     * {"deviceId":14,"elementId:"STEEL_1","locationX":1,"locationY":0,"locationZ":0},
     * {"deviceId":15,"elementId:"STEEL_1","locationX":2,"locationY":0,"locationZ":0},
     * {"deviceId":16,"elementId:"STEEL_1","locationX":3,"locationY":0,"locationZ":0}]}]},
     * "zone2":{"stations":[{"stationId":1,"targets":[{"deviceId":1,"elementId:"STEEL_1","locationX":0,"locationY":0,"locationZ":0},
     * {"deviceId":2,"elementId:"STEEL_1","locationX":1,"locationY":0,"locationZ":0},
     * {"deviceId":3,"elementId:"STEEL_1","locationX":2,"locationY":0,"locationZ":0},
     * {"deviceId":4,"elementId:"STEEL_1","locationX":3,"locationY":0,"locationZ":0}]}]}}
     * */

    //For each station in UI_Manager stationsList, create a stationTargetMapping object and add it to the dictionary
    public void CreateTargetMapping(int stationNumber)
    {
 
      Debug.Log("station number is " + stationNumber);  
      //iterate every station in the payload. If the stationId matches the stationNumber, then add the targets to the stationTargetMapping object
      foreach (var station in targetMappingdata["zone1"]["stations"])
        {
                if (station["stationId"].ToString() == stationNumber.ToString())
            {
                Debug.Log("Found matching station in payload " + stationNumber);
                foreach (JObject target in station["targets"])
                {
                    //Debug.Log("Found a target " + target["deviceId"] + "and element is " + target["elementId"]);
                    //stationTargetMapping.AddTarget(target["elementId"].ToString(), target);
                    StationTarget stationTarget = new StationTarget(stationNumber, target["deviceId"].ToString(), target["deviceId"].ToString());
                    Debug.Log("Found a target " + target["deviceId"]);
                    stationTargets.Add(stationTarget);
                    StationTargetPairs.Add(target["deviceId"].ToString(), stationNumber);
                    //new target data
                    TargetData targetData = new TargetData();
                    targetData.name = target["deviceId"].ToString();
                    targetData.location= new Vector3 (0,0,0);

                    //TargetManager.Instance.CreateVirtualTarget(targetData);
                }
             }
        }
    }
    //public void CreateAllTargetsFromGameController()
    //{

       
    //    //iterate every station in the payload. If the stationId matches the stationNumber, then add the targets to the stationTargetMapping object
    //    foreach (var station in targetMappingdata["zone1"]["stations"])
    //    {
           
    //            foreach (JObject target in station["targets"])
    //            {

    //            StationTargetPairs.Add(target["deviceId"].ToString(), int.Parse(station["stationId"].ToString()));
    //            //new target data
    //            TargetData targetData = new TargetData();
    //                targetData.name = target["deviceId"].ToString();
    //            targetData.location = new Vector3((Screen.width / 2), (Screen.height / 2), 0);

    //                TargetManager.Instance.CreateVirtualTarget(targetData);
    //            }
    //        }
    // }
    




    public int stationNumberOfTarget(string TargetName)
    {
        //return station number for input target name
        return StationTargetPairs[TargetName];

    }

}

using Newtonsoft.Json.Linq;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class StationTargetMapping
{
    //Here we will maintain a dictionary to save the targets mapping.
    //The key will be the deviceID of the target and the value will be the JObject containing details of that target.

    public static Dictionary<string, JObject> stationTargetMapping = new Dictionary<string, JObject>();

    public int stationNumber;
    public int deviceId;
    public string elementId;
    //Constructor
    

}

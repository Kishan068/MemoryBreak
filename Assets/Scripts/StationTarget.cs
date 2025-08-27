using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationTarget 
{
    private int stationId = 0;
    private string deviceId = "";
    private string elementId = "";

    //get and set stationId
    public int StationId
    {
        get { return stationId; }
        set { stationId = value; }
    }

    //get and set deviceId
    public string DeviceId
    {
        get { return deviceId; }
        set { deviceId = value; }
    }

    //get and set elementId
    public string ElementId
    {
        get { return elementId; }
        set { elementId = value; }
    }
    
    //Constructor
    public StationTarget(int stationId, string deviceId, string elementId)
    {
        this.stationId = stationId;
        this.deviceId = deviceId;
        this.elementId = elementId;
    }

}

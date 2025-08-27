using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSelection : MonoBehaviour
{
    //singleton 
    public static TargetSelection instance;

    //awake
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //Get random targets from a specified station 
    public string GetSingleRandomTarget(int stationNumber)
    {
        //from stationTargets list, filter the targets for the stationNumber and pick a random station target
        List<StationTarget> stationTargetsForStation = TargetMapping.instance.stationTargets.FindAll(x => x.StationId == stationNumber);
        int randomTarget = Random.Range(0, stationTargetsForStation.Count);
        Debug.Log("Random target is " + stationTargetsForStation[randomTarget].ElementId);
        return stationTargetsForStation[randomTarget].ElementId;
    }

    public List<string> GetMultipleRandomTarget(int stationNumber, int numberOfTargets)
    {
        //from stationTargets list, filter the targets for the stationNumber and pick a random station target
        List<StationTarget> stationTargetsForStation = TargetMapping.instance.stationTargets.FindAll(x => x.StationId == stationNumber);
        //get numberOfTargets random targets. iterate numberOfTargets times and pick a random target each time. Do not pick the same target twice
        List<string> randomTargets = new List<string>();
        
            while (randomTargets.Count<=numberOfTargets && stationTargetsForStation.Count>numberOfTargets)
            {
                int randomTarget = Random.Range(0, stationTargetsForStation.Count);
                    if (!randomTargets.Contains(stationTargetsForStation[randomTarget].ElementId))
                    {
                        randomTargets.Add(stationTargetsForStation[randomTarget].ElementId);
                    }
            }
            
           
       
        return randomTargets;
       
    }

    public List<string> GetMultipleSpecificTargets(int stationNumber, List<string> locations )
    {
        List<GameObject> stationTargets = UI_Manager.Instance.stationsList[stationNumber - 1].GetComponent<StationManager>().targetLocationsList;
        //in each stationTarget, get the TargetAtLocation component and get the targetNameText. See if this is in the locations list. If yes, add it to the list of targets

        List<string> targets = new List<string>();
        foreach (GameObject stationTarget in stationTargets)
        {
            string targetName = stationTarget.GetComponent<TargetAtLocation>().myLocationNumber.ToString();
            if (locations.Contains(targetName) && stationTarget.GetComponent<TargetAtLocation>().virtualTarget)
            {
                targets.Add(stationTarget.GetComponent<TargetAtLocation>().virtualTarget.targetNameText.text);
                Debug.Log("adding target " + stationTarget.GetComponent<TargetAtLocation>().virtualTarget.targetNameText.text + " to the list of targets" + " location is " + targetName);
            }
            //else if(!stationTarget.GetComponent<TargetAtLocation>().virtualTarget)
            //{
            //    targets.Clear();
            //    return targets;
            //}
        }

        return targets;

    }
}

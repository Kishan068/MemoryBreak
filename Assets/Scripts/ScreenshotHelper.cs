using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotHelper : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
     if(Input.GetKeyDown(KeyCode.A))
        {
            Debug.Log("A key was pressed.");
            startAllTargets();
            
        }
     if(Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log("S key was pressed.");
            start12Targets();
        }
    }

    public void startAllTargets()
    {
        Dictionary<string, PhysicalTarget> newTargetDict = new Dictionary<string, PhysicalTarget>();
        newTargetDict = TargetManager.Instance.targetDictionary;
        foreach (KeyValuePair<string, PhysicalTarget> target in newTargetDict)
        {
            TargetManager.Instance.StartTarget(target.Key,0);
        }
    }

    public void start12Targets()
    {
        Dictionary<string, PhysicalTarget> newTargetDict = new Dictionary<string, PhysicalTarget>();
        newTargetDict = TargetManager.Instance.targetDictionary;
        int i = 0;
        foreach (KeyValuePair<string, PhysicalTarget> target in newTargetDict)
        {
            if (i < 12)
            {
                TargetManager.Instance.StartTarget(target.Key, 0);
                i++;
            }
        }
    }
}

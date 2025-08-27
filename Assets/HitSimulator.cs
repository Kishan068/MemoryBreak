using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class HitSimulator : MonoBehaviour
{
    private void Start()
    {
        // Get the Button component attached to this GameObject
        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.AddListener(OnButtonClicked);
        }
    }

    private void OnButtonClicked()
    {
        // Get the VirtualTarget component attached to this GameObject
        VirtualTarget vt = GetComponent<VirtualTarget>();
        if (vt != null)
        {
            //TargetManager.Instance.HitTargetNew(vt.laneNumber, vt.targetName);
            SocketCommunicator.Instance.SendSimulatedHit(vt.targetName);
        }
    }

    //Simulate a hit on the target 
    /*{
    "deviceId": "DEVICE_ID",
    "elementId": "STEEL_1",
    "hit": {
    "hit_sequence": 100
    },
    "ipAddress": "192.168.1.10"
    }
    */
    // This method simulates a hit on the target by calling the HitTargetNew method in TargetManager.

    //Send a socket message to the server to simulate a hit on the target
   


}

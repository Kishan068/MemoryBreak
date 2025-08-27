using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class HitPoint : MonoBehaviour
{
   public bool isHit = false;
    public Dictionary<int, List<string>> LaneTargets = new Dictionary<int, List<string>>();
    public float StartDelay =0f;

    public bool IsEnabled = false;
    private void OnEnable()
    {
        //set the child object as active
        //transform.GetChild(0).gameObject.SetActive(true);
        IsEnabled = false;
    }
    
    private void OnMouseDown()
    {
        isHit = true;
    }

    float timer = 0;
    public float TimeOut = 30f;

    private void FixedUpdate()
    {
        if(IsEnabled == false)
        {
            timer += Time.deltaTime;
            if (timer >= StartDelay)
            {
                OnDemandTargetStarter.Instance.ClearAndAddInstructions(LaneTargets, TimeOut);
            }
            IsEnabled = true;

        }

    }


}

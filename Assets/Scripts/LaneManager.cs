using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaneManager : MonoBehaviour
{
    //singleton
    public static LaneManager instance;



    //awake
    private void Awake()
    {
        instance = this;
    }

    //Store all lanes
    public Dictionary<int, Lane> Lanes = new Dictionary<int, Lane>();


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

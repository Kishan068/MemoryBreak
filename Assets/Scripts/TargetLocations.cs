using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetLocations : MonoBehaviour
{
    public static TargetLocations Instance;

    private void Awake()
    {
        Instance = this;
    }


    public List<Transform> targets;
    // Start is called before the first frame update
    void Start()
    {
        GameObject[] TargetArray = GameObject.FindGameObjectsWithTag("targetWall");
        foreach (GameObject t in TargetArray)
        {
            targets.Add(t.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

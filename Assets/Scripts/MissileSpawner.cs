using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
   //singleton
   public static MissileSpawner instance;

    public GameObject missilePrefab;
    public List<GameObject> missiles = new List<GameObject>();


    //awake
    private void Awake()
    {
        instance = this;
    }
}

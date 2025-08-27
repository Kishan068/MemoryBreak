using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneScript : MonoBehaviour
{
    //singleton
    public static SceneScript instance;

    //awake
    private void Awake()
    {
        if (instance == null)
        instance = this;

        //if there is another instance destroy it
        else if (instance != this)
        {
            Destroy(gameObject);
            instance = this;
        }
            

    }

    public GameObject VirtualTarget;

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestruct : MonoBehaviour
{//destroy this gameobject after 3 seconds
    void Start()
    {
        Destroy(gameObject, 3f);
    }
}

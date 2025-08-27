using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autoDestruct : MonoBehaviour
{
   //AutoDestruct object after 5 seconds
   void Start()
    {
        Destroy(gameObject, 5);
    }
}

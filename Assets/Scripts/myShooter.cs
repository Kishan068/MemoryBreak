using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class myShooter : MonoBehaviour
{
    public GameObject shooterObject;
    public GameObject bulletPrefab;

    public void Shoot(GameObject hitTarget)
    {
        //Make the shooter object look at the target
        shooterObject.transform.LookAt(hitTarget.transform.position);

        //instantiate hit explosion in this object
        GameObject bullet = Instantiate(bulletPrefab, shooterObject.transform);
        bullet.transform.parent = Camera.main.transform;
       
    }
}

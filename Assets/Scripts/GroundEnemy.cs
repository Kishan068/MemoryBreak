using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    public GameObject ExplosionPrefab;
    public List<string> myTargetIds = new List<string>();
    public Vector3 explosionOffset;

    public List<HitPoint> hitPoints;
    public Animator Enemy_With_HalfHitAnimation;
    public void Explode()
    {
        GameObject explosion = Instantiate(ExplosionPrefab, transform.position, transform.rotation);
        explosion.transform.position += explosionOffset;
        Destroy(explosion, 1.0f);
        this.gameObject.SetActive(false);
    }
    public int PlayerNumber = 1;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Explode();
        }
    }

    public void Hit()
    {
        //check if all hitpoints are hit
        bool allHit = true;
        foreach (HitPoint hitPoint in hitPoints)
        {
            if (!hitPoint.isHit)
            {
                allHit = false;
                break;
            }
            if (Enemy_With_HalfHitAnimation != null)
            {
                Enemy_With_HalfHitAnimation.SetTrigger("Hit");
            }   
        }
        if (allHit)
        {
            //make all hit points false
            foreach (HitPoint hitPoint in hitPoints)
            {
                hitPoint.isHit = false;
            }
            Explode();

        }
    }


}

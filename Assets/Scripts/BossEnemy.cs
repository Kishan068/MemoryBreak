using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    public Animator myAnim;
    public Transform LarvaeSpawnPoint;
    public List<GameObject> LarvaePrefabs;

    public List<GameObject> WeakPointPrefabs;
    public List<Transform> WeakPointLocation;


    public List<Transform> LeftWeakPointLocation;

 
    public List<Transform> RightWeakPointLocation;
    public void spawnLarvae(int count)
    {
        myAnim.SetTrigger("spawn");
        for (int i = 0; i < count; i++)
        {
            //for every prefab, instantiate it at the spawn point
            foreach (GameObject prefab in LarvaePrefabs)
            {
                Instantiate(prefab, LarvaeSpawnPoint.position, Quaternion.identity);
            }

            
        }
       
    }

    public void leftAttack()
    {
        myAnim.SetTrigger("leftAttack");
    }

    public void rightAttack()
    {
        myAnim.SetTrigger("rightAttack");
    }

    public void death()
    {
        myAnim.SetTrigger("death");
    }

    public void castSpell()
    {
        myAnim.SetTrigger("cast");
    }

    public void showWeakPoint()
    {
        //spawn a weakpoint at each location. WeakPointPrefabs[0] will be spawned at WeakPointLocation[0] and so on
        for (int i = 0; i < WeakPointPrefabs.Count; i++)
        {

            Instantiate(WeakPointPrefabs[i], WeakPointLocation[i].position, Quaternion.identity);
        }

        myAnim.SetTrigger("weakpoint");
    }

    public void showLeftWeakPoints()
    {
        //spawn a weakpoint at each location. WeakPointPrefabs[0] will be spawned at WeakPointLocation[0] and so on
        for (int i = 0; i < WeakPointPrefabs.Count; i++)
        {
            //Instantiate and parent the weakpoint to the left weakpoint location
            GameObject weakPoint = Instantiate(WeakPointPrefabs[i], LeftWeakPointLocation[i].position, Quaternion.identity);
            weakPoint.transform.parent = LeftWeakPointLocation[i];
            //Move the weakpoint to 1 unit closer to the main camera
            weakPoint.transform.Translate(Vector3.forward * -1);


        }

        myAnim.SetTrigger("leftAttack");

    }

    public void showRightWeakPoints()
    {
        //spawn a weakpoint at each location. WeakPointPrefabs[0] will be spawned at WeakPointLocation[0] and so on
        for (int i = 0; i < WeakPointPrefabs.Count; i++)
        {
            //Instantiate and parent the weakpoint to the left weakpoint location
            GameObject weakPoint = Instantiate(WeakPointPrefabs[i], RightWeakPointLocation[i].position, Quaternion.identity);
            weakPoint.transform.parent = RightWeakPointLocation[i];
            weakPoint.transform.Translate(Vector3.forward * -1);


        }

        myAnim.SetTrigger("rightAttack");

    }

    public void Hit()
    {
        myAnim.SetTrigger("hit");
    }

    public void BossEnabled()
    {
        myAnim.SetTrigger("enable");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            myAnim.SetTrigger("hit");
        }

    }

    
}

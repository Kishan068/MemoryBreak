using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WitchEnemy : MonoBehaviour
{
    public Animator myAnim;

    public GameObject SpellPrefab;

    public Transform SpellSpawnPoint;


    //create a function to spawn spells
    public void SpawnSpell(int spellCount)
    {
        //Spawn as many spells as we want
        for (int i = 0; i < spellCount; i++)
        {
            myAnim.SetTrigger("attack");
            //Spawn a spell
            GameObject spell = Instantiate(SpellPrefab, SpellSpawnPoint.position, Quaternion.identity);
        }

    }


}

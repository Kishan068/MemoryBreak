using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalTarget : MonoBehaviour
{
    public string targetName;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    public void StartTarget()
    {
       AudioManager.Instance.PlayStartTarget();
            Debug.Log("Start Target");
        animator.ResetTrigger("Stop");
        animator.ResetTrigger("Hit");
        animator.SetTrigger("Start");
    }

    public void StopTarget()
    {
      Debug.Log("Stop Target");
        animator.SetTrigger("Stop");
        
    }

    public void HitTarget()
    {
        animator.SetTrigger("Hit");
    }
}

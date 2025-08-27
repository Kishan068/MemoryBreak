using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateMyself : MonoBehaviour
{
   //
   public Animator Animator;

    public void IntroAnimation()
    {
        Animator.SetTrigger("Intro");
    }

    public void DeathAnimation()
    {
        Animator.SetTrigger("Death");
    }


}

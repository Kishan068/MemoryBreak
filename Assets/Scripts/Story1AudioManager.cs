using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Story1AudioManager : MonoBehaviour
{
    public AudioSource PortalAudioSource;
    public AudioSource LaserAudioSource;
    public AudioSource HitAudioSource;
    public AudioSource ShieldAudioSource;
    public AudioSource Villain_Laugh_Source;
    public AudioSource BipedSpawnSound;
    public AudioSource FinalExplosionAudioSource;
    public AudioSource VillainPowerupSource;
    public AudioSource QuadSpawnSource;
    public AudioSource WatchOut;
    public AudioSource Pain;
    public AudioSource TakingCasualties;
    public AudioSource EnemyDown;
    public AudioSource EngageTheEnemy;
    public AudioSource GoGoGo;
    public AudioSource wellDone;


    public void PlayPortalAudio()
    {
        PortalAudioSource.Play();
    }

    public void PlayLaserAudio()
    {
        LaserAudioSource.Play();
    }

    public void PlayHitAudio()
    {
        HitAudioSource.Play();
    }

    public void PlayShieldAudio()
    {
        ShieldAudioSource.Play();
    }

    public void PlayVillainLaughAudio()
    {
        Villain_Laugh_Source.Play();
    }

    public void PlayFinalExplosionAudio()
    {
        FinalExplosionAudioSource.Play();
    }

    public  void PlayBipedSpawnSound()
    {
        BipedSpawnSound.Play();
    }

    public void PlayVillainPowerupSound()
    {
        VillainPowerupSource.Play();
    }

    public void PlayQuadSpawnSound()
    {
        QuadSpawnSource.Play();
    }

    public void PlayWatchOut()
    {
        WatchOut.Play();
    }

    public void PlayPain()
    {
        Pain.Play();
    }

    public void PlayTakingCasualties()
    {
        TakingCasualties.Play();
    }

    public void PlayEnemyDown()
    {
        EnemyDown.Play();
    }

    public void PlayEngageTheEnemy()
    {
        EngageTheEnemy.Play();
    }

    public void PlayGoGoGo()
    {
        GoGoGo.Play();
    }

    public void PlayWellDone()
    {
        wellDone.Play();
    }
        

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton instance for global access
    public static AudioManager Instance;

    // Audio sources for playing sound effects and ambience
    public AudioSource audioSource;
    public AudioSource GameAmbienceAudioSource;

    // Sound effect and ambience clips
    public AudioClip demoModeClip;
    public AudioClip CountdownClip;
    public AudioClip GameOverClip;
    public AudioClip GetReadyClip;
    public AudioClip StartShootingClip;
    public AudioClip GameClip;
    public AudioClip IdleAmbience;

    // Variations for explosion and target start sounds
    public List<AudioClip> explosionSounds;
    public List<AudioClip> startTargetSounds;

    // Volume levels
    public float AmbientVolume = 0.3f;
    public float GameVolume = 0.4f;

    // Timestamp to resume paused ambience
    public float timeWhenAmbiencePaused = 0f;

    // Set up singleton and play idle ambience at startup
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        GameAmbienceAudioSource.clip = IdleAmbience;
        GameAmbienceAudioSource.volume = AmbientVolume;
        GameAmbienceAudioSource.Play();
    }

    // === Public Methods to Play Specific Sounds ===

    public void PlayCountdown()
    {
        PlayClip(audioSource, CountdownClip);
    }

    public void PlayGameOver()
    {
        PlayClipIfNotPlaying(audioSource, GameOverClip);
    }

    public void PlayGetReady()
    {
        PlayClip(audioSource, GetReadyClip);
    }

    public void PlayStartShooting()
    {
        PlayClipIfNotPlaying(audioSource, StartShootingClip);
    }

    public void PlayGame()
    {
        PlayClipIfNotPlaying(GameAmbienceAudioSource, GameClip, GameVolume);
    }

    public void PlayIdleAmbience()
    {
        PlayClipIfNotPlaying(GameAmbienceAudioSource, IdleAmbience, AmbientVolume);
    }

    public void EnterDemoMode()
    {
        PlayClip(GameAmbienceAudioSource, demoModeClip, AmbientVolume);
    }

    public void ExitDemoMode()
    {
        PlayClipIfNotPlaying(GameAmbienceAudioSource, IdleAmbience, AmbientVolume);
    }

    public void PlayExplosion()
    {
        PlayRandomClip(audioSource, explosionSounds);
    }

    public void PlayStartTarget()
    {
        PlayRandomClip(audioSource, startTargetSounds);
    }

    public void StopAudio()
    {
        audioSource.Stop();
        PlayIdleAmbience();
    }

    public void StopAllAudio()
    {
        audioSource.Stop();
        GameAmbienceAudioSource.Stop();
    }

    // === Helper Methods ===

    // Plays a specific AudioClip on the given source with optional volume
    private void PlayClip(AudioSource source, AudioClip clip, float volume = 1.0f)
    {
        if (clip == null) return;

        source.clip = clip;
        source.volume = volume;
        source.Play();
    }

    // Plays clip only if it's not already playing on the source
    private void PlayClipIfNotPlaying(AudioSource source, AudioClip clip, float volume = 1.0f)
    {
        if (clip == null || (source.clip == clip && source.isPlaying)) return;

        PlayClip(source, clip, volume);
    }

    // Plays a random clip from the provided list
    private void PlayRandomClip(AudioSource source, List<AudioClip> clips)
    {
        if (clips == null || clips.Count == 0) return;

        int index = Random.Range(0, clips.Count);
        PlayClip(source, clips[index]);
    }
}

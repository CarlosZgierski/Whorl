using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    private AudioSource ActiveAudioSource;

    private AudioSource ExplorationAudioSource;
    [SerializeField]
    float ExplorationFullVolume = 0.5f;
    private AudioSource CombatAudioSource;
    [SerializeField]
    float CombatFullVolume = 0.6f;

    public float TransitionDuration = 2F;

    void Start()
    {
        AudioSource[] x = GetComponents<AudioSource>();
        ExplorationAudioSource = x[0];
        CombatAudioSource = x[1];
        ActiveAudioSource = ExplorationAudioSource;


    }

 
    public void ChangeMusicToExploration()
    {
        StartCoroutine(Transition(CombatAudioSource, ExplorationAudioSource));
    }

    public void ChangeMusicToCombat()
    {
        StartCoroutine(Transition(ExplorationAudioSource, CombatAudioSource));
    }

    IEnumerator Transition(AudioSource x, AudioSource y)
    {
        float xVolumeMultiplier;
            float yVolumeMultiplier;

        if (y == CombatAudioSource)
        {
            xVolumeMultiplier = ExplorationAudioSource.volume / TransitionDuration;
            yVolumeMultiplier = CombatFullVolume / TransitionDuration;
        }
        else
        {
            xVolumeMultiplier = CombatAudioSource.volume / TransitionDuration;
            yVolumeMultiplier = ExplorationFullVolume / TransitionDuration;
        }
      
        float transitionEndTime = Time.time + TransitionDuration;

        
        while(Time.time < transitionEndTime)
        {
            x.volume -= Time.deltaTime * xVolumeMultiplier;
            y.volume += Time.deltaTime * yVolumeMultiplier;
            yield return null;
        }
      
    }

}

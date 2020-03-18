using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SoundModule : MonoBehaviour
{
    public Transform ParentOfInstantiatedAudioSources;

    [SerializeField]
    ClipSet HurtClipSet;

    [SerializeField]
    ClipSet DeathClipSet;

    [SerializeField]
    ClipSet FootstepClipSet;

    [SerializeField]
    ClipSet DodgeClipSet;

    [SerializeField]
    AnimationEvents_Struggler StrugglerAnimationEvents;
    [SerializeField]
    ClipSet AttackClipSet;


    AudioSource audioSource;
    AudioSource AudioSourceFootstep;

    HealthController HealthController;
    MovementModule MovementModule;

    [Space]
    [SerializeField]
    AudioMixerGroup audioMixer;


    void Start()
    {
        audioSource = CreateChildWithAudioSource("AudioSource (SoundModule)");

        HealthController = GetComponent<HealthController>();
        MovementModule = GetComponent<MovementModule>();

        if(StrugglerAnimationEvents && AttackClipSet)
        {
            StrugglerAnimationEvents.OnPlayAttackSound += PlayAttackSoundAnimationEvent;
        }

        if (FootstepClipSet)
        {
            AudioSourceFootstep = CreateChildWithAudioSource("AudioSourceFootsteps (SoundModule)");
            audioSource.outputAudioMixerGroup = audioMixer;

            if(MovementModule)
            {
                MovementModule.OnMove += PlayFootstepSoundEvent;
                MovementModule.OnStill += StopFootstepSoundEvent;
            }
        }

        if(DodgeClipSet && MovementModule) MovementModule.OnDodge += PlayDodgeSoundEvent;
        if(DeathClipSet) HealthController.OnDie += DieEvent;
        if(HurtClipSet)  HealthController.OnDamage += TakeDamageEvent;

    }

    #region Events

    void PlayDodgeSoundEvent()
    {
        PlayFromClipSet(DodgeClipSet);
    }

    void PlayFootstepSoundEvent()
    {
        if (!AudioSourceFootstep.isPlaying)
        {
            Clip x = FootstepClipSet.GetRandomClip();
            AudioSourceFootstep.clip = x.AudioClip;
            AudioSourceFootstep.volume = x.volume;
            AudioSourceFootstep.Play();
        }
    }

    void StopFootstepSoundEvent()
    {
        if(AudioSourceFootstep.isPlaying)
        {
            AudioSourceFootstep.Stop();
        }
    }

    void PlayAttackSoundAnimationEvent()
    {
        PlayFromClipSet(AttackClipSet);
    }

    void TakeDamageEvent(float damage, DamageType p, HealthController x)
    {
        if(damage > 0)
        {
            PlayFromClipSet(HurtClipSet);
        }
        else if(damage == 0)
        {

        }
        else
        {

        }
    }

    void DieEvent(HealthController x)
    {
        PlayClipAtPointFromClipSetInPosition(DeathClipSet);
    }

    #endregion

    #region PrivateMethods

    AudioSource CreateChildWithAudioSource(string name)
    {
        GameObject x = new GameObject(name);
        x.transform.position = transform.position;
        if(ParentOfInstantiatedAudioSources == null)
        {
            ParentOfInstantiatedAudioSources = transform;
        }
        x.transform.parent = ParentOfInstantiatedAudioSources;
        AudioSource p = x.AddComponent<AudioSource>();
        p.outputAudioMixerGroup = audioMixer;
        return p;
    }

    void PlayClipAtPointFromClipSetInPosition(ClipSet x)
    {
        AudioSource newAudioSource = new GameObject("DeathAudioSource", typeof(AudioSource)).GetComponent<AudioSource>();
        AudioClip clip = x.GetRandomClip().AudioClip;
        newAudioSource.clip = clip;
        newAudioSource.outputAudioMixerGroup = audioMixer;
        newAudioSource.Play();
        Destroy(newAudioSource.gameObject, clip.length);
    }


    void PlayFromClipSet(ClipSet clipset)
    {
        Clip x = clipset.GetRandomClip();
        audioSource.PlayOneShot(x.AudioClip, x.volume);
    }

    #endregion


}

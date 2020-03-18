using System;
using UnityEngine;

public class AnimationEvents_Struggler : MonoBehaviour
{
    public TrailRenderer dodgeTrail;
    public event Action OnAddDodgeForceEvent = delegate { };

    public event Action OnLowSpeedEvent = delegate { };
    public event Action OnFullSpeedEvent = delegate { };
    public event Action OnZeroSpeedEvent = delegate { };

    public event Action OnLowRotationSpeedEvent = delegate { };
    public event Action OnFullRotationSpeedEvent = delegate { };
    public event Action OnZeroRotationSpeedEvent = delegate { };

    public event Action OnSetCastingToFalse = delegate { };
    public event Action OnSetCastingToTrue = delegate { };

    public event Action OnSetAttackingToFalse = delegate { };
    public event Action OnSetAttackingToTrue = delegate { };
    public event Action OnPlayAttackSound = delegate { };

    public GameEvent DeathEvent;

    [SerializeField]
    AudioSource AudioSourceToPlay;

    [SerializeField]
    ParticleSystem ParticleToPlay;

    public void PlayParticle()
    {
        ParticleToPlay.Play();
    }


    public void AddDodgeForceEvent()
    {
        OnAddDodgeForceEvent();
    }

    public void SetTrailRendererEmitingToTrue()
    {
        dodgeTrail.emitting = true;
    }

    public void SetTrailRendererEmitingToFalse()
    {
        dodgeTrail.emitting = false;
    }

    public void SetSpeedToLow()
    {
        OnLowSpeedEvent();
    }

    public void SetSpeedToFull()
    {
        OnFullSpeedEvent();
    }

    public void SetSpeedToZero()
    {
        OnZeroSpeedEvent();
    }


    public void SetRotationSpeedToLow()
    {
        OnLowRotationSpeedEvent();
    }

    public void SetRotationSpeedToFull()
    {
        OnFullRotationSpeedEvent();
    }

    public void SetRotationSpeedToZero()
    {
        OnZeroRotationSpeedEvent();
    }

    public void SetCastingToTrue()
    {
        OnSetCastingToTrue();
    }

    public void SetCastingToFalse()
    {
        OnSetCastingToFalse();
    }

    public void TurnVuneralibityOn()
    {

        transform.root.gameObject.GetComponent<HealthController>().SetInvulnerabilityTo(true);

        
    }

    public void TurnVuneralibityOff()
    {
        transform.root.gameObject.GetComponent<HealthController>().SetInvulnerabilityTo(false);
    }

    public void PlayAttackSound()
    {
        OnPlayAttackSound();
    }

    public void SetAttackingToTrue()
    {
        OnSetAttackingToTrue();
    }

    public void DeathStuff()
    {
        SetRotationSpeedToZero();
        SetSpeedToZero();
        
        HealthController x;
        x = transform.root.GetComponent<HealthController>();
        x.gameObject.GetComponent<InputModule>().enabled = false;
        x.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        Destroy(x);
        transform.root.tag = "Untagged";
        Destroy(x.transform.Find("Canvas").gameObject);

    }

    public void CallDeathEvent()
    {
        DeathEvent.Raise();
    }

    public void SetAttackingToFalse()
    {
        OnSetAttackingToFalse();
    }


}

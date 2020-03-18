using System;
using UnityEngine;

public class AnimationEvents_Enemy : MonoBehaviour
{
    public event Action OnAttackEnded = delegate { };
    public event Action OnHitAnimationEnd = delegate { };
    public event Action OnDeathAnimationEnd = delegate { };
    public event Action OnActivationAnimationEnd = delegate { };

    [SerializeField]
    ScreenShakerReference ScreenShakerReference;
    [SerializeField]
    float screenShakeAmount = 1f;
    [SerializeField]
    float screenShakeDuration = 1f;

    [SerializeField]
    AudioSource audioSourceToPlay;




    public void AttackEnd()
    {
        OnAttackEnded();
    }

    public void HitAnimationEnd()
    {
        OnHitAnimationEnd();
    }

    public void DeathAnimationEnd()
    {
        OnDeathAnimationEnd();
    }

    public void ActivationAnimationEnd_()
    {
        OnActivationAnimationEnd();
    }

    public void PlayAudioSource()
    {
        audioSourceToPlay.Play();
    }

    public void TriggerScreenShake()
    {
        ScreenShakerReference.value.ShakeCamera(screenShakeDuration, screenShakeAmount);
    }



}

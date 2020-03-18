using System;
using UnityEngine;

public class AnimationModule_Enemy : MonoBehaviour
{
    public Animator Animator;

    HealthController HealthController;
    MovementModule_Navigation MovementModule;

    [SerializeField]
    AnimationEvents_Enemy AnimationEvents;

    public event Action OnAttackAnimationEnded = delegate { };
    public event Action OnHitAnimationStart = delegate { };
    public event Action OnHitAnimationEnd = delegate { };
    public event Action OnDeathAnimationEnd = delegate { };
    public event Action OnActivationAnimationEnd = delegate { };

    [SerializeField]
    bool ImplementDirectionalDamageAnimation = false;

    [SerializeField]
    bool ImplementWalking = true;
    [SerializeField]
    bool ImplementTakeHit = true;


    private void Start()
    {

        MovementModule = GetComponent<MovementModule_Navigation>();
        if(MovementModule)
        {
            if (ImplementWalking)
            {
                MovementModule.OnMovementModuleDisabled += SetWalkingToFalse;
                MovementModule.OnMovementModuleEnabled += SetWalkingToTrue;
            }
        }

        if(AnimationEvents)
        {
            AnimationEvents.OnAttackEnded += AttackEndedAnimationEvent;
            AnimationEvents.OnHitAnimationEnd += HitAnimationEndAnimationEvent;
            AnimationEvents.OnDeathAnimationEnd += DeathAnimationEndAnimationEvent;
            AnimationEvents.OnActivationAnimationEnd += ActivationAnimationEndAnimationEvent;
        }

        HealthController = GetComponent<HealthController>();
        HealthController.OnDie += Die;
        if (ImplementTakeHit)
        {
            HealthController.OnDamageWithPosition += TakeDamage;
        }
        
    }

  

    private void TakeDamage(float a, DamageType b, HealthController c, Vector3? damageBoxPosition)
    {
        if(ImplementDirectionalDamageAnimation)
        {
            SetAllDamageDirectionParametersToFalse();
            GetAngleFromDamageSource(damageBoxPosition);
        }
        Animator.SetTrigger("TakeDamage");
        
        if(AnimationEvents)
        {
            OnHitAnimationStart();
            AnimationEvents.OnHitAnimationEnd += HitAnimationEndAnimationEvent;
        }

    }

    private void GetAngleFromDamageSource(Vector3? x)
    {
        Vector3 pos;
        if (x == null)
        {
            return; 
        }
        else
        {
            pos = (Vector3)x;
        }
        float angle = Vector3.SignedAngle(transform.forward, pos - transform.position, Vector3.up);
        float angleAbs = Mathf.Abs(angle);
        if (angleAbs < 25f)
        {
            Animator.SetBool("DamageFromFront", true);

        }
        else
        {
            if (angle > 0 && angle < 130f)
            {
                Animator.SetBool("DamageFromRight", true);
            }
            else if (angle > -130f)
            {
                
                Animator.SetBool("DamageFromLeft", true);
            }

        }
    }

    private void SetAllDamageDirectionParametersToFalse()
    {
        Animator.SetBool("DamageFromLeft", false);
        Animator.SetBool("DamageFromFront", false);
        Animator.SetBool("DamageFromRight", false);
    }

    private void DeathAnimationEndAnimationEvent()
    {
        OnDeathAnimationEnd();
    }

    private void HitAnimationEndAnimationEvent()
    {
        OnHitAnimationEnd();
    }

    private void Die(HealthController x)
    {
        Animator.SetTrigger("Dead");
    }

    public void SetAttackTrigger()
    {
        Animator.SetTrigger("Attack");
    }

    public void SetAttack2Trigger()
    {
        Animator.SetTrigger("Attack2");
    }

    public void SetActiveBoolTo(bool x)
    {
        Animator.SetBool("Active", x);
    }

    public void SetCastTrigger()
    {
        Animator.SetTrigger("Cast");
    }

    void ActivationAnimationEndAnimationEvent()
    {
        OnActivationAnimationEnd();
    }

    private void AttackEndedAnimationEvent()
    {
        OnAttackAnimationEnded();
    }

    private void SetWalkingToFalse()
    {
        Animator.SetBool("Walking", false);
    }

    private void SetWalkingToTrue()
    {
        Animator.SetBool("Walking", true);
    }
}


using UnityEngine;
using System.Collections;

public class AnimationModule : MonoBehaviour
{
    
    MovementModule MovementModule;
    AttackModule AttackModule;
    SpellCaster_Struggler SpellCaster;
    

    [SerializeField]
    private Animator animator;
    private Rigidbody Rigidbody;

    private HealthController HealthController;
    Coroutine SetWalkingToFalseCoroutine;


    private void Start()
    {
        
        MovementModule = GetComponent<MovementModule>();
        MovementModule.OnMove += Walking;
        MovementModule.OnStill += Idle;
        MovementModule.OnDodge += Dodge;

        SpellCaster = GetComponent<SpellCaster_Struggler>();
        SpellCaster.OnTriggerCastTargetAnimation += CastTargetSpell;
        
        AttackModule = GetComponent<AttackModule>();
        AttackModule.OnAttack += Attack;

        HealthController = GetComponent<HealthController>();
        HealthController.OnDie += TriggerDie;

        Rigidbody = GetComponent<Rigidbody>();
    }

    void TriggerDie(HealthController x)
    {
        animator.SetTrigger("Death");
    }

    void Dodge()
    {
        animator.SetTrigger("Dodge");
    }

    void CastTargetSpell()
    {
        animator.SetTrigger("CastTarget");
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
    }

    void Walking()
    {
        if (SetWalkingToFalseCoroutine != null)
        {
            StopCoroutine(SetWalkingToFalseCoroutine);
            SetWalkingToFalseCoroutine = null;
        }
        
        animator.SetBool("MoveUp", false);
        animator.SetBool("MoveDown", false);
        animator.SetBool("MoveRight", false);
        animator.SetBool("MoveLeft", false);
        
        if(MovementModule.CurrentDirection == MovementModule.WalkingDirections.Front)
        {
            animator.SetBool("MoveUp", true);
        }
        else if(MovementModule.CurrentDirection == MovementModule.WalkingDirections.Back)
        {
            animator.SetBool("MoveDown", true);
        }
        else if (MovementModule.CurrentDirection == MovementModule.WalkingDirections.Right)
        {
            animator.SetBool("MoveRight", true);
        }
        else
        {
            animator.SetBool("MoveLeft", true);
        }
        
        animator.SetBool("Walking", true);
    }

    void Idle()
    {
        if (SetWalkingToFalseCoroutine == null)
        {
            SetWalkingToFalseCoroutine = StartCoroutine(SetWalkingToFalse());
        }

    }

    IEnumerator SetWalkingToFalse()
    {
        yield return new WaitForSeconds(0.2f);
        animator.SetBool("Walking", false);
        SetWalkingToFalseCoroutine = null;
    }

    

  
    public void SetCastingTo(bool x)
    {
        animator.SetBool("Casting", x);
    }

    

    
}

using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class AttackModule_Enemy : MonoBehaviour
{

    AnimationModule_Enemy AnimationModule;

    public event Action OnAttackEnd = delegate { };


    [SerializeField]
    AttackMoveSelectionTypes AttackMoveSelectionType = AttackMoveSelectionTypes.Single;

    [SerializeField]
    AttackMove[] AttackMoves;
    private int currentAttackIndex = 0;

    enum AttackMoveSelectionTypes
    {
        Single, Random, Pattern
    }


    void Start()
    {
        AnimationModule = GetComponent<AnimationModule_Enemy>();

    }



    public void EnableToAttackByIndex(int attackIndex)
    {
        UseAttackMove(attackIndex);
    }

    public void EnableToAttack()
    {

        enabled = true;

        if(AttackMoveSelectionType == AttackMoveSelectionTypes.Single)
        {
            SingleAttack();
        }
        else if (AttackMoveSelectionType == AttackMoveSelectionTypes.Pattern)
        {
            PatternAttack();
        }
        else
        {
            RandomAttack();
        }
        

    }

    void SingleAttack()
    {
        UseAttackMove(currentAttackIndex);
    }

    void PatternAttack()
    {
        if (currentAttackIndex > AttackMoves.Length - 1)
        {
            currentAttackIndex = 0;
        }
        UseAttackMove(currentAttackIndex);
        currentAttackIndex++;
    }

    void UseAttackMove(int Index)
    {
        AttackMoves[Index].AttackMoveUnityEvent.Invoke();
        if(AttackMoves[Index].AttackType == AttackTypes.Animated)
        {
            AnimationModule.OnAttackAnimationEnded += AttackAnimationEndedEvent;
        }
        else if(AttackMoves[Index].AttackType == AttackTypes.Spell)
        {
            StartCoroutine(EndAttackCoroutine(AttackMoves[Index].SpellUseDuration));
        }
    }

    void RandomAttack()
    {
        currentAttackIndex = UnityEngine.Random.Range(0, AttackMoves.Length);
        UseAttackMove(currentAttackIndex);

    }

    IEnumerator EndAttackCoroutine(float duration)
    {
        yield return new WaitForSeconds(duration);
        EndAttack();
    }

    void AttackAnimationEndedEvent()
    {
        EndAttack();
    }



    void EndAttack() {
        if (this)
        {
            if (AnimationModule)
            {
                AnimationModule.OnAttackAnimationEnded -= AttackAnimationEndedEvent;
            }
            OnAttackEnd();
            enabled = false;
        }


    }


    public enum AttackTypes
    {
        Animated, Spell, Other
    }

    [Serializable]
    class AttackMove
    {
       
        public AttackTypes AttackType;
        public float SpellUseDuration;
        public UnityEvent AttackMoveUnityEvent;
    }
}

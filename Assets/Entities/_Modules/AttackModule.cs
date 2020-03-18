using System;
using UnityEngine;

public class AttackModule : MonoBehaviour
{
    public bool attacking = false;
    InputModule InputModule;
    SpellCaster_Struggler SpellCaster;
    public AnimationEvents_Struggler AnimationEvents;

    public event Action OnAttack = delegate { };
    public event Action OnAttackEnd = delegate { };
    

    void Start()
    {

        attacking = false;
        InputModule = GetComponent<InputModule>();
        SpellCaster = GetComponent<SpellCaster_Struggler>();
        InputModule.OnMouseButtonPressed += Attack;
        AnimationEvents.OnSetAttackingToFalse += SetAttackingToFalse;
        AnimationEvents.OnSetAttackingToTrue += SetAttackingToTrue;

    }

    void Attack()
    {
       
        if(!SpellCaster.casting)
        OnAttack();
    }

    void SetAttackingToTrue()
    {
        attacking = true;
    }

    void SetAttackingToFalse()
    {
        attacking = false;
        OnAttackEnd();
    }


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextAttackTemporaryEffect : MonoBehaviour
{

    EffectsModule EffectsModule;
    AttackModule AttackModule;
    [SerializeField]
    Effect Effect;

    void Start()
    {
        EffectsModule = GetComponent<SpellInstance>().spellCaster.GetComponent<EffectsModule>();
        AttackModule = EffectsModule.gameObject.GetComponent<AttackModule>();
        AttackModule.OnAttackEnd += LoseBoost;
        EffectsModule.ApplyTemporaryEffect(Effect);

    }

    void LoseBoost()
    {
        AttackModule.OnAttackEnd -= LoseBoost;
        EffectsModule.RemoveTemporaryEffect(Effect);
        Destroy(gameObject);

    }

   
}

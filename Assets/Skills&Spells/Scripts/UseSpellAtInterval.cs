using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpellCaster))]
public class UseSpellAtInterval : MonoBehaviour
{

    public float timeInterval = 1;
    public int spellIndex;
    SpellCaster x;
    NavMeshBrain brain;
    bool canFire = true;

    private void Start()
    {
        brain = GetComponent<NavMeshBrain>();
        if(brain)
        {
            canFire = false;
            brain.OnTargetOnAttackRange += Enable;
            brain.OnStartChasingTarget += Disable;
        }
        x = GetComponent<SpellCaster>();
        InvokeRepeating("UseSpell", timeInterval, timeInterval);
    }
    private void Disable(GameObject x)
    {
        canFire = false;
    }

    private void Enable(GameObject x)
    {
        canFire = true;
    }

    private void UseSpell()
    {
        if(canFire)
        x.UseSpell(spellIndex);
    }


}

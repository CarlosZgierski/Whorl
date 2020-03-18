using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellInstance : MonoBehaviour
{
    public GameObject caster;
    public SpellCaster spellCaster;




    public void Initialize(SpellCaster caster)
    {
        this.caster = caster.gameObject;
        spellCaster = caster;
    }
}

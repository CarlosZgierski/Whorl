using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Skill/Spell/InstantiatedSpell")]
public class InstantiatedSpell : Spell
{
    [SerializeField]
    GameObject SpellPrefab;
    [SerializeField]
    bool InstatiateWithSameRotationAsCaster;
    [SerializeField]
    bool InstatiateAsChild;


    override public void Use(SpellCaster x)
    {
        GameObject i = InstantiateSpell(x);
        if(i.TryGetComponent(out SpellInstance spellInstance))
        {
            spellInstance.Initialize(x);
        }
    }

    GameObject InstantiateSpell(SpellCaster x)
    {

        GameObject i = Instantiate(SpellPrefab, x.transform.position, Quaternion.identity);
        if (InstatiateWithSameRotationAsCaster)
        {
            i.transform.rotation = x.transform.rotation;
        }
        if (InstatiateAsChild)
        {
            i.transform.parent = x.InstantiatedSpellsParent;
        }
        return i;
    }
}

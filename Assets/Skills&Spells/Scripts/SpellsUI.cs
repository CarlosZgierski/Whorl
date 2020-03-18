using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellsUI : MonoBehaviour
{
    [SerializeField]
    SpellCaster_Struggler SpellCaster;

    [SerializeField]
    SpellIcon[] Icons;

    Dictionary<Spell, SpellIcon> IconBySpellDictionary = new Dictionary<Spell, SpellIcon>();

    private void Start()
    {
        if(SpellCaster)
        {
            SpellCaster.OnSpellAdded += AddSpellToUI;
            SpellCaster.OnSpellRemoved += RemoveSpellFromUI;
        }
    }

    private void RemoveSpellFromUI(Spell x)
    {
        foreach (SpellIcon p in Icons)
        {
            if (p.spell == x)
            {
                p.Disable();
                IconBySpellDictionary.Remove(x);
                break;
            }
        }
    }

    private void AddSpellToUI(Spell x)
    {
        foreach (SpellIcon p in Icons)
        {
            if (!p.hasSpell)
            {
                p.SetIcon(x);
                p.SubscribeToCooldownEventOnSpellWrapper(SpellCaster.SpellWrapperBySpellDictionary[x]);
                IconBySpellDictionary.Add(x, p);
                return;
            }
        }
        Debug.LogWarning("Not enough SpellIcons to OwnedSpells in SpellCaster_Struggler");
    }

}

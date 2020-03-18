using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Aura))]
public class VampiricAura : MonoBehaviour
{
    HealthController AuraCaster;
    Aura Aura;
    public DamageType healType;
    [SerializeField]
    float fractionOfDamageToHeal = 0.5f;

    float intervalBetweenDamage = 0.4f;
    float timeToBecomeABleToDamageAgain = 0;

    private void Start()
    {
        AuraCaster = GetComponent<SpellInstance>().caster.GetComponent<HealthController>();
    }


    private void OnEnable()
    {
        
        Aura = GetComponent<Aura>();

        if (Aura.AffectedEntities.Count > 0)
        {
            foreach (HealthController x in Aura.AffectedEntities)
            {
                AddVampiricAffect(x);
            }
        }
        Aura.OnEntityAffected += AddVampiricAffect;
        Aura.OnEntityReleased += RemoveVampiricAffect;
    
    }

    private void OnDisable()
    {
        foreach (HealthController x in Aura.AffectedEntities)
        {
            RemoveVampiricAffect(x);
        }
    }

    private void AddVampiricAffect(HealthController x)
    {
        x.OnDamage += HealVampire;
        x.OnDie += RemoveVampiricAffect;
    }

    private void RemoveVampiricAffect(HealthController x)
    {
        x.OnDamage -= HealVampire;
        x.OnDie -= RemoveVampiricAffect;
    }

    void HealVampire(float damage, DamageType type, HealthController x)
    {
        if (Time.time > timeToBecomeABleToDamageAgain)
        {
           
            timeToBecomeABleToDamageAgain = Time.time + intervalBetweenDamage;
            AuraCaster.DoDamage(- damage * fractionOfDamageToHeal, healType, transform.position);
        }
    }
}

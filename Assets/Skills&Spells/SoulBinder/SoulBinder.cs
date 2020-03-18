using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Aura))]
public class SoulBinder : MonoBehaviour
{
    Aura Aura;
    public DamageType sharedDamageType;
    [SerializeField]
    float intervalBetweenDamage = 0.5f;
    float timeToBecomeABleToDamageAgain = 0;

    private void OnEnable()
    {
        Aura = GetComponent<Aura>();
        if (Aura.AffectedEntities.Count > 0)
        {
            foreach (HealthController x in Aura.AffectedEntities)
            {
                BindSoul(x);
            }
        }
        Aura.OnEntityAffected += BindSoul;
        Aura.OnEntityReleased += UnbindSoul;
    }

    private void OnDisable()
    {
        foreach (HealthController x in Aura.AffectedEntities)
        {
            UnbindSoul(x);
        }
    }

    private void BindSoul(HealthController x)
    {
        x.OnDamage += ShareDamage;
        x.OnDie += UnbindSoul;
    }

    private void UnbindSoul(HealthController x)
    {
        x.OnDamage -= ShareDamage;
        x.OnDie -= UnbindSoul;
    }

    void ShareDamage(float damage, DamageType type, HealthController DamagedHealthController)
    {
        if (Time.time > timeToBecomeABleToDamageAgain)
        {
            timeToBecomeABleToDamageAgain = Time.time + intervalBetweenDamage;

            float damageShare = damage / Aura.AffectedEntities.Count;
            damageShare = damageShare * 2f;
            DamagedHealthController.DoDamage(-damage, type,transform.position);
#if UNITY_EDITOR
            Debug.Log("total damage: " + damage.ToString() + " share: " + damageShare.ToString() + " .Number of affectted : " +  Aura.AffectedEntities.Count);
#endif
            for (int i = Aura.AffectedEntities.Count - 1; i >= 0; i--)
            {
            
                Aura.AffectedEntities[i].DoDamage(damageShare, sharedDamageType,transform.position);
            }
        }
        

    }

}

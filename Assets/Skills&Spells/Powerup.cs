using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField]
    Spell PowerupSpell;
    [SerializeField]
    bool onlyPickIfFullLife;
    [SerializeField]
    GameObject gameObjectToDestroy;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Health"))
        {
 
            if(other.gameObject.TryGetComponent(out HealthController x))
            {
                if (x.entityType == EntityTypes.Player)
                {
                    if(onlyPickIfFullLife && x.health >= x.MaxHealth)
                    {
                     
                        return;
                    }
                    x.gameObject.GetComponent<SpellCaster_Struggler>().UseSpellOneTime(PowerupSpell);
                    Destroy(gameObjectToDestroy);
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectralDagger : MonoBehaviour
{
    public SpellInstance SpellInstance;
    public DamageBox damageBox;
    public Spell spell;
    public GameObject shadowTeleportOriginParticle;
    public GameObject shadowTeleportDestinyParticle;
    // Start is called before the first frame update
    void Start()
    {
        damageBox.OnHit += Hit;
    }

    private void Hit(HealthController x)
    {
        if(x.Health <= 0 || x.health <= x.MaxHealth/3)
        {
            x.DoDamage(100f, null, null);
            GameObject casterGameObject = SpellInstance.caster.gameObject;
            Instantiate(shadowTeleportOriginParticle, casterGameObject.transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
            casterGameObject.GetComponent<Rigidbody>().MovePosition(x.gameObject.transform.position);
            Instantiate(shadowTeleportDestinyParticle, x.gameObject.transform.position, Quaternion.Euler(new Vector3(0f, 0f, 0f)));
            if (SpellInstance.spellCaster is SpellCaster_Struggler)
            {
                SpellCaster_Struggler struggler = SpellInstance.spellCaster as SpellCaster_Struggler;
                struggler.ResetCooldown(spell);
            }
           
        }
    }
}

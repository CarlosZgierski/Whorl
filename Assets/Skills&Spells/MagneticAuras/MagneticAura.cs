using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagneticAura : MonoBehaviour
{

    public Transform AuraCasterTransform;
    Aura Aura;
    List<Rigidbody> AffectedRigidBodies = new List<Rigidbody>();

    public float force;


    private void Start()
    {
        AuraCasterTransform = gameObject.GetComponent<SpellInstance>().caster.transform;
        DestroyPastMagneticAura();
        
        Aura = GetComponent<Aura>();
        if (Aura.AffectedEntities.Count > 0)
        {
            foreach (HealthController x in Aura.AffectedEntities)
            {
                AddToMagneticField(x);
            }
        }
        Aura.OnEntityAffected += AddToMagneticField;
        Aura.OnEntityReleased += RemoveFromMagneticField;

    }

    void DestroyPastMagneticAura()
    {
        MagneticAura[] past = AuraCasterTransform.GetComponentsInChildren<MagneticAura>();
        foreach(MagneticAura x in past)
        {
            if(x != this)
            {
                Destroy(x.gameObject);
            }
        }
   
    }

    private void FixedUpdate()
    {
        foreach (Rigidbody x in AffectedRigidBodies)
        {
            x.AddForce((AuraCasterTransform.position - x.transform.position)* force * Time.smoothDeltaTime); ;
        }
    }

    private void OnDisable()
    {
        AffectedRigidBodies.Clear();
    }

    private void AddToMagneticField(HealthController x)
    {
        AffectedRigidBodies.Add(x.GetComponent<Rigidbody>());
    }

    private void RemoveFromMagneticField(HealthController x)
    {
        AffectedRigidBodies.Remove(x.GetComponent<Rigidbody>());
    }

    
}

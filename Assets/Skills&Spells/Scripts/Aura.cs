
using UnityEngine;
using System.Collections.Generic;
using System;

public class Aura : MonoBehaviour
{
    [SerializeField]
    List<HealthController> affectedEntities = new List<HealthController>();
    [SerializeField]
    GameObjectVariable ImmuneEntity;


    public Action<HealthController> OnEntityAffected = delegate { };
    public Action<HealthController> OnEntityReleased = delegate { };

    public bool OnlyAffectOneEntityType = false;
    public EntityTypes OnlyAffectedType;

    public List<HealthController> AffectedEntities
    {
        get
        {
            return affectedEntities;
        }

    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        HealthController x = other.GetComponent<HealthController>();
        if (x)
        {
            if (OnlyAffectOneEntityType && x.entityType == OnlyAffectedType || !OnlyAffectOneEntityType)
            {
                AffectEntity(x);
            }


        }
    }

    private void AffectEntity(HealthController x)
    {
        affectedEntities.Add(x);
        x.OnDie += ReleaseEntity;
        OnEntityAffected(x);
    }

    private void ReleaseEntity(HealthController x)
    {
        affectedEntities.Remove(x);
        x.OnDie -= ReleaseEntity;
        OnEntityReleased(x);
    }

    private void OnDestroy()
    {
        CleanAffectedEntities();
    }

    private void CleanAffectedEntities()
    {
        for(int i = affectedEntities.Count - 1; i > 0; i--)
        {
            ReleaseEntity(affectedEntities[i]);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        HealthController x = other.GetComponent<HealthController>();
        if (x)
        {
            if (OnlyAffectOneEntityType && x.entityType == OnlyAffectedType || !OnlyAffectOneEntityType)
            {
                ReleaseEntity(x);
            }
        }
    }

}

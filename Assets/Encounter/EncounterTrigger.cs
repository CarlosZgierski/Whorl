using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterTrigger : MonoBehaviour
{
    
    public Encounter EncounterToTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Health"))
        {

            if(other.GetComponent<HealthController>().entityType == EntityTypes.Player)
            {
                TriggerEncounter();
            }
     
        }
    }

    private void TriggerEncounter()
    {
        EncounterToTrigger.StartEncounter();
    }
}

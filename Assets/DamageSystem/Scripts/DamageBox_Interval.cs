using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBox_Interval : MonoBehaviour
{

    public float timeInterval;
    public bool hitPlayer;
    public bool hitEnemies;
    public float damageReference;
    public DamageType damageType;
    Dictionary<HealthController, Coroutine> HealthToCoroutineDicionary = new Dictionary<HealthController, Coroutine>();



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Health"))
        {
            HealthController x = other.gameObject.GetComponent<HealthController>();
            if ((hitEnemies && x.entityType == EntityTypes.Enemy) || (
                hitPlayer && x.entityType == EntityTypes.Player))
            {

                HealthToCoroutineDicionary.Add(x, StartCoroutine(DoDamage(x)));
                x.OnDie += RemoveHealthControllerFromDictionary;

            }

        }
    }

    private void RemoveHealthControllerFromDictionary(HealthController x)
    {
        //StopCoroutine(HealthToCoroutineDicionary[x]);
        HealthToCoroutineDicionary.Remove(x);
    }

    private IEnumerator DoDamage(HealthController x)
    {
        while (x)
        {
            x.DoDamage(damageReference, damageType, transform.position);
            yield return new WaitForSeconds(timeInterval);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Health"))
        {
            HealthController x = other.gameObject.GetComponent<HealthController>();
            if ((hitEnemies && x.entityType == EntityTypes.Enemy) || (
                hitPlayer && x.entityType == EntityTypes.Player))
            {
                if(HealthToCoroutineDicionary.ContainsKey(x))
                {
                    StopCoroutine(HealthToCoroutineDicionary[x]);
                    HealthToCoroutineDicionary.Remove(x);
                }

            }

        }
    }



}


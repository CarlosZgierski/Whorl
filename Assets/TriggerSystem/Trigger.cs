
using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField]
    UnityEvent unityEvent;

    [SerializeField]
    bool TriggerByPlayer = true;
    [SerializeField]
    bool TriggerByEnemy = true;
    [SerializeField]
    bool TriggerByNeutral = true;
    [SerializeField]
    bool DestroyOnTrigger = false;
  
    private void OnTriggerEnter(Collider other)
    {
        HealthController x = other.GetComponent<HealthController>();
        if(x)
        {
            if(TriggerByPlayer)
            {
                if(x.entityType == EntityTypes.Player)
                {
                    Triggered();
                }
            }
            if(TriggerByNeutral)
            {
                if (x.entityType == EntityTypes.Neutral)
                {
                    Triggered();
                }
            }
            if(TriggerByEnemy)
            {
                if (x.entityType == EntityTypes.Enemy)
                {
                    Triggered();
                }
            }
        }
    }

    void Triggered()
    {
        unityEvent.Invoke();
        if(DestroyOnTrigger)
        {
            Destroy(gameObject);
        }
    }
}

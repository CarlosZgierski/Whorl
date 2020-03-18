using UnityEngine.Events;
using UnityEngine;

public class DeathAdditionalBehaviour : MonoBehaviour
{
    [SerializeField]
    UnityEvent DeathUnityEvent;

    void Start()
    {
        GetComponent<HealthController>().OnDie += DeathEventHandler;
    }


    void DeathEventHandler(HealthController x)
    {
        DeathUnityEvent.Invoke();
    }
}

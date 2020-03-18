using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    HealthController HealthController;
    [SerializeField]
    GameObject SmokeInstance;

    // Start is called before the first frame update
    void Start()
    {
        
        HealthController = GetComponent<HealthController>();
        HealthController.OnDie += Die;
    }


    void Die(HealthController x)
    {
        Instantiate(SmokeInstance, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}

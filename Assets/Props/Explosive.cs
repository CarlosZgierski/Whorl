using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosive : MonoBehaviour
{
    public GameObject explosionPrefab;
    [SerializeField]
    bool destroyThisGameObjectWhenExplode = true;

    void Start()
    {
        GetComponent<HealthController>().OnDie += Explode;
    }

    // Update is called once per frame
    void Explode(HealthController x)
    {
        Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        if (destroyThisGameObjectWhenExplode)
        {
            Destroy(gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileDefender : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        var p = other.GetComponent<Projectile>();
        if(p)
        {
            Destroy(p.gameObject);
            Destroy(gameObject);
        }
    }
}

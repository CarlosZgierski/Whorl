using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    public GameObject deathParticle;
    public GameObject damageParticle;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        HealthController x = GetComponent<HealthController>();
        x.OnDie += Die;
        //x.OnDamage += Damage;/
    }

    void Damage()
    {
       //Instantiate(damageParticle, transform.position, transform.rotation);
    }

    private void Update()
    {

    }

    void Die(HealthController x)
    {
        Instantiate(deathParticle, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}

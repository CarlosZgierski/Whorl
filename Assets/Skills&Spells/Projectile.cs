using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    private float timeToDie;
    [SerializeField]
    bool DestroyOnWallHit = false;
   

    public GameObject particlePrefabToInstantiateOnDestroy;


    private void OnEnable()
    {
        timeToDie = Time.time + lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
        if(Time.time > timeToDie)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
   
        if (DestroyOnWallHit && (other.CompareTag("Wall")  || other.CompareTag("Health")))
        {
            Destroy(gameObject);
            if (particlePrefabToInstantiateOnDestroy)
            {
                Instantiate(particlePrefabToInstantiateOnDestroy, transform.position, transform.rotation);
            }
        }
    }


}

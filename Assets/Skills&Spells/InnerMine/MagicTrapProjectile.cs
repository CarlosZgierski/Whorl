using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MagicTrapProjectile : MonoBehaviour
{

    public float speed;
    public float lifeTime;
    private float timeToDie;
    public GameObject MagicTrapPrefab;
    


    private void OnEnable()
    {
        timeToDie = Time.time + lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
        if (Time.time > timeToDie)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Health"))
        {
            HealthController x = other.GetComponent<HealthController>();
            if(x.entityType == EntityTypes.Neutral)
            {
                Transform p = Instantiate(MagicTrapPrefab, x.transform.position, Quaternion.identity).transform;
                p.parent = x.transform;
                Destroy(gameObject);

            }
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }


    }


}


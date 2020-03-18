using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class LaserProjectile : MonoBehaviour
{
    public float maxDistance = 20f;
    LineRenderer LineRenderer;
    public LayerMask LayerMask;
    public float RefreshRate = 0.1f;
    public float DamageRate = 0.7f;
    public float damage = 0.5f;

    HealthController HealthControllerToDamage;


    void Start()
    {
        LineRenderer = GetComponent<LineRenderer>();
        LineRenderer.SetPosition(0, transform.position);
        StartCoroutine(DrawLine());
        StartCoroutine(Damage());
   

        
    }

    IEnumerator DrawLine()
    {
        while (true)
        {

            Ray ray = new Ray(transform.position, transform.forward);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 20f, LayerMask))
            {
                LineRenderer.SetPosition(1, hit.point);
                if (hit.transform.CompareTag("Health"))
                {
                    HealthControllerToDamage = hit.transform.gameObject.GetComponent<HealthController>();
                }
                else
                {
                    HealthControllerToDamage = null;
                }
            }
            else
            {
                HealthControllerToDamage = null;
                LineRenderer.SetPosition(1, transform.forward * maxDistance);
            }
            yield return new WaitForSeconds(RefreshRate);
        }
    }

    IEnumerator Damage()
    {
        while(true)
        {
            if (HealthControllerToDamage)
            {
                HealthControllerToDamage.DoDamage(damage, null, transform.position);
            }
            yield return new WaitForSeconds(DamageRate);
        }
    }

}

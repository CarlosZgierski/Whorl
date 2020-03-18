
using System.Collections.Generic;
using UnityEngine;

public class RicochetingBall : MonoBehaviour
{
    GameObject target;
    [SerializeField]
    int TimesCanHit = 2;
    [SerializeField]
    GameObjectVariable immuneTarget;
    [SerializeField]
    LayerMask LayerMask;
    [SerializeField]
    float Radius = 2;

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Health"))
        {
            if ((other.gameObject != immuneTarget.Value))
            {
               /*if(target == null)
                {
                    target = other.gameObject;
                }
                */
                if(true)
                //if (other.gameObject == target)
                {
                    List<HealthController> possibleTargets = new List<HealthController>();
                    HealthController x = other.gameObject.GetComponent<HealthController>();

                    Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius);

                    TimesCanHit--;

                    if (TimesCanHit <= 0)
                    {
                        Destroy(gameObject);
                    }


                    foreach (Collider c in hitColliders)
                    {
                        if (c.CompareTag("Health") && c.gameObject != immuneTarget.Value)
                        {
                            possibleTargets.Add(c.GetComponent<HealthController>());
                        }
                    }

                    if (possibleTargets.Count <= 1)
                    {
                        Destroy(gameObject);
                    }
                    else {



                            possibleTargets.Remove(other.gameObject.GetComponent<HealthController>());
                            target = possibleTargets[Random.Range(0, possibleTargets.Count)].gameObject;
                            MyFunctions.LookAtOnlyYAxis(transform, target.transform.position);
                            if(target == other.gameObject)
                        {
                            Destroy(gameObject);
                        }
                            
                        }
                       
                        
                    }
                }
            

            
        }
     
    }

  
}

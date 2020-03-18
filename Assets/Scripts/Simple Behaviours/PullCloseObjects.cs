
using UnityEngine;

 public class PullCloseObjects : MonoBehaviour
{
    public float pullRadius = 2;
    public float pullForce = 1;

    public void FixedUpdate()
    {
        foreach (Collider collider in Physics.OverlapSphere(transform.position, pullRadius))
        {
            // calculate direction from target to me
            Rigidbody x = collider.GetComponent<Rigidbody>();
            if (x)
            {
                Vector3 forceDirection = transform.position - collider.transform.position;

                // apply force on target towards me
                x.AddForce(forceDirection.normalized * pullForce * Time.fixedDeltaTime);
            }
        }
    }
}

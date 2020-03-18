using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Health"))
        {
            transform.GetChild(0).gameObject.SetActive(true);
            Invoke("StopTalking", 4);
        }
    }

    void StopTalking()
    {
        transform.GetChild(0).gameObject.SetActive(false);
        Destroy(this);
    }
}

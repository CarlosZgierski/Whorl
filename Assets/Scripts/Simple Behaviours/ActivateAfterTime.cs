using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAfterTime : MonoBehaviour
{
    public GameObject GameObjectToActivate;
    public float TimeToActivate;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Activate", TimeToActivate);
    }

    public void Activate()
    {
        GameObjectToActivate.SetActive(true);
    }

}

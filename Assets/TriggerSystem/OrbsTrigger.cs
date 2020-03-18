using System;
using UnityEngine;

public class OrbsTrigger : MonoBehaviour
{
    public event Action OnActivation = delegate { };



    public void ActivateOrb()
    {
        OnActivation();
        Destroy(gameObject);
    }
}

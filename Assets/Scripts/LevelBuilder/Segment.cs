using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    public Transform Exit;
    [SerializeField]
    private GameObject sacrificeTriggerGameObject;

    public void ActivateSacrificeTrigger()
    {
        sacrificeTriggerGameObject.SetActive(true);
    }
    

   
}

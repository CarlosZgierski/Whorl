using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogDistanceBetweenTwoObjects : MonoBehaviour
{
    public Transform object1;
    public Transform object2;
 

    void Update()
    {
        if(object1 && object2)
        Debug.Log(Vector3.Distance(object1.position, object2.position));
    }
}

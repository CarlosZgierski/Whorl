using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRotationOnUpdate : MonoBehaviour
{
    public Vector3 desiredRotation = new Vector3(37,0,0);
    Quaternion rotationInQuaternion;

    private void Awake()
    {
        rotationInQuaternion = Quaternion.Euler(desiredRotation);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.rotation = rotationInQuaternion;
    }
}

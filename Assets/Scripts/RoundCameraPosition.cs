using System;
using UnityEngine;

public class RoundCameraPosition : MonoBehaviour
{
    private void Update()
    {

        transform.position = new Vector3((float)Math.Truncate(100 * transform.position.x) / 100,
            transform.position.y,
            (float)Math.Truncate(100 * transform.position.z) / 100);
    }

   
}

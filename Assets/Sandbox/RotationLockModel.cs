using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationLockModel : MonoBehaviour
{
    [SerializeField]
    Transform realTransform;
    public float lockValue = 45f;

    private void Start()
    {
        if (realTransform == null)
        {
            realTransform = transform.root;
        }
    }

    void Update()
    {
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        float p = (int)(realTransform.eulerAngles.y / lockValue) * lockValue;
        transform.Rotate(0,p,0, Space.World);
        
        if(realTransform.eulerAngles.y - p >lockValue / 2)
        {
            transform.Rotate(0, lockValue, 0, Space.World);
        }
        

       
    }
}

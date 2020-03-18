using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateOnStart : MonoBehaviour
{
    [SerializeField]
    GameObject prefab;

    private void Start()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }
}

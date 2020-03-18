using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAsGameObjectVariable : MonoBehaviour
{
    public GameObjectVariable GameObjectReference;

    private void Awake()
    {
        GameObjectReference.Value = gameObject;
    }

    private void OnEnable()
    {
        GameObjectReference.Value = gameObject;
    }

    private void OnDestroy()
    {
        GameObjectReference.Value = null;
    }
}

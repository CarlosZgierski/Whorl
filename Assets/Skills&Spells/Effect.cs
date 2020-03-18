
using UnityEngine;

abstract public class Effect : ScriptableObject
{
    abstract public void Apply(GameObject target);
    abstract public void Remove(GameObject target);
}

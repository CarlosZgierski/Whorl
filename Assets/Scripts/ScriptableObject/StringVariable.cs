using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "ScriptableObjects/StringVariable")]
public class StringVariable : ScriptableObject
{
    [SerializeField]
    [Multiline]
    private string DeveloperDescription;
    [Space]
    [SerializeField]
    protected string value;

    public virtual string Value
    {
        get
        {
            return value;
        }
        set
        {
            this.value = value;
        }
    }

    public static implicit operator string(StringVariable reference)
    {
        return reference.Value;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DamageTypeResistance
{
    [SerializeField]
    public DamageType DamageType;
    [SerializeField]
    public float ResistanceValue = 0;

    public DamageTypeResistance(DamageType type, float Value)
    {
        DamageType = type;
        ResistanceValue = Value;
    }
}

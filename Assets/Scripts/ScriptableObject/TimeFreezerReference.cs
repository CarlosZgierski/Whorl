using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/TimeFreezerReference")]
public class TimeFreezerReference : ScriptableObject
{
    public TimeFreezer value;

    public void FreezeTime(float timeToFreeze)
    {
        value.Freeze(timeToFreeze);
    }
}

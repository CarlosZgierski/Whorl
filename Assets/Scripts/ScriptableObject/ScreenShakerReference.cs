using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/ScreenShakerReference")]
public class ScreenShakerReference : ScriptableObject
{
    public ScreenShaker value;

    public void Shake(float duration, float shakeAmount)
    {
        value.ShakeCamera(duration, shakeAmount);
    }
}

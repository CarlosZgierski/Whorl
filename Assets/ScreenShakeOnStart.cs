using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShakeOnStart : MonoBehaviour
{
    [SerializeField]
    ScreenShakerReference screenShakerReference;

    [SerializeField]
    float screenShakeDuration;
    [SerializeField]
    float screenShakeAmount;

    void Start()
    {
        screenShakerReference.Shake(screenShakeDuration, screenShakeAmount);
    }

   
}

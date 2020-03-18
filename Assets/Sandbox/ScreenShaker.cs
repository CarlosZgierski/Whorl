using UnityEngine;
using System.Collections;

public class ScreenShaker : MonoBehaviour
{
    
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    public bool shaketrue = false;

    public ScreenShakerReference screenShakerReference;

    Vector3 originalPos;

    void Start()
    {
        screenShakerReference.value = this;
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
        
    }

    void Update()
    {
        if (shaketrue)
        {
            if (shakeDuration > 0)
            {
                camTransform.localPosition = Vector3.Lerp(camTransform.localPosition, originalPos + Random.insideUnitSphere * shakeAmount, Time.deltaTime * 3);

                shakeDuration -= Time.deltaTime * decreaseFactor;
            }
            else
            {
                camTransform.localPosition = originalPos;
                shaketrue = false;
            }
        }
    }

 

    public void ShakeCamera(float _shakeDuration, float _shakeAmount)
    {
        shaketrue = true;
        shakeDuration = _shakeDuration;
        shakeAmount = _shakeAmount;
    }
}
using UnityEngine.Events;
using UnityEngine;

public class UnityEventOnStartAfterTime : MonoBehaviour
{
    public float time;
    public UnityEvent unityEvent;

    void Start()
    {
        Invoke("Invoke", time);
       
    }



    void Invoke()
    {
        unityEvent.Invoke();
    }

   
}

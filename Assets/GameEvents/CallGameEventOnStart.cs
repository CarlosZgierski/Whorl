using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallGameEventOnStart : MonoBehaviour
{
    public GameEvent GameEvent;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return null;
        GameEvent.Raise();
    }
}
    

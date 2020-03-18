using System;
using UnityEngine;

public class OnDestroyDetachGamesObjectstAndInvokeDestroy : MonoBehaviour
{
    [SerializeField]
    TrailRenderer[] trailRenderersToDetach;
    [SerializeField]
    ParticleSystem[] particlesToDetach;
    [SerializeField]
    AudioSource[] audioSourcesToDetach;
    [SerializeField]
    GenericGameObjectToDetach[] genericGameObjectsToDetach;

    private void OnDestroy()
    {
       
        foreach(TrailRenderer x in trailRenderersToDetach)
        {
            x.transform.parent = null;
            x.autodestruct = true;

        }
        foreach(AudioSource x in audioSourcesToDetach)
        {
            x.transform.parent = null;
            Destroy(x.gameObject, x.clip.length - x.time);
        }
        foreach(ParticleSystem x in particlesToDetach)
        {
            x.transform.parent = null;
            Destroy(x.gameObject, x.main.duration + x.main.startLifetimeMultiplier);
        }
        foreach(GenericGameObjectToDetach x in genericGameObjectsToDetach)
        {
            x.gameObject.transform.parent = null;
            Destroy(x.gameObject, x.timeToDestroy);
        }
    }

    [Serializable]
    public class GenericGameObjectToDetach
    {
        public GameObject gameObject;
        public float timeToDestroy;
    }

 

}

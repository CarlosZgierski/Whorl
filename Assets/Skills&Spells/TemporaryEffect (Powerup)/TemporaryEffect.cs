using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryEffect : MonoBehaviour
{

    EffectsModule EffectsModule;
    [SerializeField]
    Effect Effect;
    [SerializeField]
    float duration = 10f;

    void Start()
    {
        EffectsModule = GetComponent<SpellInstance>().spellCaster.GetComponent<EffectsModule>();
        EffectsModule.ApplyTemporaryEffect(Effect);
        StartCoroutine(LoseBoost());

    }

    IEnumerator LoseBoost()
    {
        yield return new WaitForSeconds(duration);
        EffectsModule.RemoveTemporaryEffect(Effect);
        Destroy(gameObject);

    }

    
}

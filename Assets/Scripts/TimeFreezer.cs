using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreezer : MonoBehaviour
{
    public TimeFreezerReference Reference;

    public void Start()
    {
        Reference.value = this;
    }

    public void Freeze(float time)
    {
        StartCoroutine(FreezeTime(time));
    }


    IEnumerator FreezeTime(float TimeToFreeze)
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(TimeToFreeze);
        Time.timeScale = 1;
    }
}

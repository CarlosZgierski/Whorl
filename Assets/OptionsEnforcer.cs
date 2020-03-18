using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsEnforcer : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    [TextArea]
    string DevNote = "This component on start gets the playerPrefs for volume and sets it in the audioMixer";
#endif
    [SerializeField]
    AudioMixer AudioMixer;


    // Start is called before the first frame update
    void Start()
    {
        EnforceVolumePref(SoundVolume.Master);
        EnforceVolumePref(SoundVolume.Music);
        EnforceVolumePref(SoundVolume.Ambient);
        EnforceVolumePref(SoundVolume.SFX);
    }


    void EnforceVolumePref(SoundVolume soundVolume)
    {
        string audioMixerParameter = Options.AudioMixerParameterBySoundVolume[soundVolume];
        AudioMixer.GetFloat(audioMixerParameter, out float currentVolume);
        float x = PlayerPrefs.GetFloat(audioMixerParameter, 7f);
        
        AudioMixer.SetFloat(audioMixerParameter, Mathf.Log10(MyMethods.ConvertValueFromOldScaleToNewScale(x, 0, 15, 0.0001f, 1f)) * 20);
    }
}

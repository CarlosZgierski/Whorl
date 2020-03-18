using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Options : MonoBehaviour
{
    [SerializeField]
    GameEvent OnOptionsChangeGamEvent;
    [SerializeField]
    AudioMixer audioMixer;
    public AudioMixer AudioMixer { get {return audioMixer; } }



    public static Dictionary<SoundVolume, string> AudioMixerParameterBySoundVolume = new Dictionary<SoundVolume, string>
    {
        {SoundVolume.Master,  "MasterVolume"},
        {SoundVolume.Music,  "MusicVolume"},
        {SoundVolume.Ambient,  "AmbientVolume"},
        {SoundVolume.SFX,  "SoundEffectsVolume"},
    };


    public void FullScreenChange(bool x)
    {
        Screen.fullScreen = x;

    }
    public void VolumeChange(SoundVolume volumeToChange, float newValue)
    {
        
        PlayerPrefs.SetFloat(AudioMixerParameterBySoundVolume[volumeToChange], newValue);
        AudioMixer.SetFloat(AudioMixerParameterBySoundVolume[volumeToChange], Mathf.Log10(MyMethods.ConvertValueFromOldScaleToNewScale(newValue, 0, 15, 0.0001f, 1f)) * 20);

    }
}

public enum SoundVolume { Master, Music, Ambient, SFX };




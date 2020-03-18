
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    [SerializeField]
    Slider slider;
    [SerializeField]
    Options Options;
    [SerializeField]
    public SoundVolume SoundVolume;

    private void OnEnable()
    {
        slider.value = PlayerPrefs.GetFloat(Options.AudioMixerParameterBySoundVolume[SoundVolume], 7f);
      
        VolumeChange();
       

    }

    public void VolumeChange()
    {
        Options.VolumeChange(SoundVolume, slider.value);
    }
}

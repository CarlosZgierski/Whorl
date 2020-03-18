using UnityEngine.UI;
using UnityEngine;

public class FullScreenToggle : MonoBehaviour
{
    [SerializeField]
    Options Options;
    [SerializeField]
    Toggle Toggle;

    public void ValueChange()
    {
        Options.FullScreenChange(Toggle.isOn);
    }
}

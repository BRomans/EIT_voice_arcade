using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioVisualizer : MonoBehaviour
{
    public Text volumeText;

    private float volume;
    private float volumeLast;

    void Start()
    {

    }

    void Update()
    {
        volume = GetComponent<MicrophoneInput>().GetAveragedVolume();

        volumeText.text = "dB: " + volume;

        if (Mathf.Abs(volumeLast - volume) > .05f)
        {
            volumeText.text = "volume flap";
        } else
        {
            volumeText.text = " ";
        }

        volumeLast = volume;
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class AudioVisualizer : MonoBehaviour
{
    private float volume;
    private float volumeLast;

    void Start()
    {

    }

    void Update()
    {
        volume = GetComponent<MicrophoneInput>().GetAveragedVolume();

        if (Mathf.Abs(volumeLast - volume) > .05f)
        {
            GameController.instance.birdFlap();
        }

        volumeLast = volume;
    }
}

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
        // volume varies between .001 and .36 about
        volume = GetComponent<MicrophoneInput>().GetAveragedVolume();

        // old jumping
        //if (Mathf.Abs(volumeLast - volume) > .05f)
        //{
        //    GameController.instance.birdFlap();
        //}

        // new jumping
        if (volume > .1f)
        {
            //Debug.Log("is flapping...");
            GameController.instance.flapping = true;
        } else
        {
            //Debug.Log("not flapping...");
            GameController.instance.flapping = false;
        }

        volumeLast = volume;
    }
}

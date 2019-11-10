using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/* Process audio and turn it into a visualizeable behavior, based on volume */
public class AudioVisualizer : MonoBehaviour
{
    private float volume;
    //private float volumeLast; // single jumping implementation

    void Start() {}

    /* Check the volume to see if a command has been issued */
    void Update()
    {
        // For debugging purposed volume varies between about .001 and .36
        volume = GetComponent<MicrophoneInput>().GetAveragedVolume();

        // Single jump - a change in volume was mapped to one jump
        //if (Mathf.Abs(volumeLast - volume) > .05f)
        //{
        //    GameController.instance.birdFlap();
        //}
        //volumeLast = volume;

        // Continous jump - continuously jump if volume is above 0.1, which is the level of loud talking
        if (volume > .1f)
        {
            // if volume is significant, let controller know the bird should be flapping
            GameController.instance.flapping = true;
        } else
        {
            // if volume is insignificant, let controller know the bird should not be flapping
            GameController.instance.flapping = false;
        }
    }
}

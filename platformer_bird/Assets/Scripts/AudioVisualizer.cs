using UnityEngine;
using UnityEngine.UI;
using System.Collections;

/* Process audio and turn it into a visualizeable behavior, based on volume */
public class AudioVisualizer : MonoBehaviour
{
    private float volume;
    // private float volumeLast; // single jumping implementation

    // For debugging purposes: volume varies between about .001 and .36
    private float sensitivity = .08f;
    private float highSensitivity = .05f;
    private float lowSensitivity = .12f;
    private float medSensitivity = .08f;

    void Start() {}

    /* Check the volume to see if a command has been issued,
     also update the mic sensitivity if it changes */
    void Update()
    {
        // Set the volume sensitivity from the game controller
        switch (GameController.instance.volumeSensitivity)
        {
            case "Low":
                sensitivity = lowSensitivity;
                break;
            case "Medium":
                sensitivity = medSensitivity;
                break;
            case "High":
                sensitivity = highSensitivity;
                break;
            default:
                sensitivity = medSensitivity;
                break;
        }
        
        volume = GetComponent<MicrophoneInput>().GetAveragedVolume();

        // Single jump - a change in volume was mapped to one jump
        //if (Mathf.Abs(volumeLast - volume) > .05f)
        //{
        //    GameController.instance.birdFlap();
        //}
        //volumeLast = volume;

        // Continous jump - continuously jump if volume is above 0.1, which is the level of loud talking
        if (volume > sensitivity && GameController.instance.gameStarted)
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

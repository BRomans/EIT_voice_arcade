using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class MicrophoneInput : MonoBehaviour
{
    public int audioSampleRate = 44100;
    public string microphone;

    private AudioSource audioSource;

    /* Get and set audio source */
    void Start()
    {
        // Get component for audio
        audioSource = GetComponent<AudioSource>();

        // Get all available microphones
        foreach (string device in Microphone.devices)
        {
            if (microphone == null)
            {
                // Set default mic to first mic found - built-in microphone
                microphone = device;
            }
        }

        UpdateMicrophone();
    }

    /* Get audio from mic, then play into Unity scene
    - Sound is muted for the player in Unity using an audio mixer */
    void UpdateMicrophone()
    {
        audioSource.Stop();

        //Start recording to audioclip from the mic
        audioSource.clip = Microphone.Start(microphone, true, 10, audioSampleRate);
        audioSource.loop = true;

        // Make sure the mic is recording
        if (Microphone.IsRecording(microphone))
        {
            while (!(Microphone.GetPosition(microphone) > 0))
            {
                // Wait until the recording has started...
            }

            // Start playing the audio source
            audioSource.Play();
        }
        else
        {
            Debug.Log("Mic: " + microphone + " - doesn't work!");
        }
    }

    /* Calculate the volume from an averaged across the last 256 samples */
    public float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        audioSource.GetOutputData(data, 0);
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }
}
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public class VoiceRecognitionController : MonoBehaviour
{

    private PlayerController playerController;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
        
    public VoiceRecognitionController(PlayerController controller) {
        playerController = controller;
    }
        
    /* Intialize the available voice controls in an array */
    public void setupActions() {
        actions.Add("jump", () => {
            Debug.Log("Bird is jumping");
            playerController.Jump();
        });
        actions.Add("start", () => {
            //TriggerJump()
            Debug.Log("Game has started");
            playerController.Restart();
        });

        actions.Add("die", () => {
            Debug.Log("You psycho!");
            playerController.Die();
        });

        actions.Add("bang", () => {
            Debug.Log("Pew Pew!");
            playerController.Shoot();
        });

        actions.Add("peu", () => {
            Debug.Log("Pew Pew!");
            playerController.Shoot();
        });

        actions.Add("pew", () => {
            Debug.Log("Pew Pew!");
            playerController.Shoot();
        });

    }    

    /* This function initialise the voice recognizer */
    public void setupRecognizer() {
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
        Debug.Log("Voice recognizer ready!");

    }

    /* This function triggers the voice control if it's in our dictionary */
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech) {
        Debug.Log(speech.text);
        System.Action action;

        // If the keyword is in our dictionary, call that Action.
        if (actions.TryGetValue(speech.text, out action))
        {
            action.Invoke();
        }
    }

    
}

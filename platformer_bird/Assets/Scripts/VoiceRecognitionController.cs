using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public class VoiceRecognitionController : MonoBehaviour
{
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();
        
    public VoiceRecognitionController() {
        setupActions();
        setupRecognizer();
    }
        
    /* Intialize the available voice controls in an array */
    public void setupActions() {
        actions.Add("jump", () => {
            Debug.Log("Bird is jumping");
            PlayerController.instance.Jump();
        });
        actions.Add("start", () => {
            Debug.Log("Game has started");
            GameController.instance.startGame();
        });

        actions.Add("restart", () => {
            Debug.Log("Game has restarted");
            GameController.instance.restartGame();
        });

        actions.Add("reload", () => {
            Debug.Log("Game has reloaded");
            GameController.instance.reloadGame();
        });

        actions.Add("set low", () => {
            Debug.Log("Change volume sensibility");
            GameController.instance.updateVolumeSensitivity(0);
        });

        actions.Add("set middle", () => {
            Debug.Log("Change volume sensibility");
            GameController.instance.updateVolumeSensitivity(1);
        });

        actions.Add("set high", () => {
            Debug.Log("Change volume sensibility");
            GameController.instance.updateVolumeSensitivity(2);
        });

        actions.Add("die", () => {
            Debug.Log("You psycho!");
            PlayerController.instance.Die();
        });

        actions.Add("bang", () => {
            Debug.Log("Pew Pew!");
            PlayerController.instance.Shoot();
        });

        actions.Add("peu", () => {
            Debug.Log("Pew Pew!");
            PlayerController.instance.Shoot();
        });

        actions.Add("pew", () => {
            Debug.Log("Pew Pew!");
            PlayerController.instance.Shoot();
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

using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class bird : MonoBehaviour
{
    private bool isDead = false;
    private Rigidbody2D rb2d;
    private Animator anim;

    public float upForce = 200f;
    
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        setupActions();
        setupRecognizer();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (Input.GetMouseButtonDown (0))
            {
               Jump();
            }
        }
    }

    private void OnCollisionEnter2D()
    {
        rb2d.velocity = Vector2.zero;
        isDead = true;
        anim.SetTrigger("Die");
        GameController.instance.birdDied();
    }

    private void Jump() {
        // reset velocity to zero so behavior stays consistent
        rb2d.velocity = Vector2.zero;

        rb2d.AddForce(new Vector2(0, upForce));
        anim.SetTrigger("Flap");
    }

    public bool isBirdDead()  {
        return isDead;
    }

     // voice recognition implementation

    private void setupActions() {
        actions.Add("jump", () => {
            TriggerJump();
        });
        actions.Add("start", () => {
            //TriggerJump()
        });
        
        actions.Add("test", () => {
            //TriggerJump()
        });

    }

    private void setupRecognizer() {
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
    }

    private void RecognizedSpeech(PhraseRecognizedEventArgs speech) {
        Debug.Log(speech.text);
        System.Action action;
        // if the keyword recognized is in our dictionary, call that Action.
        if (actions.TryGetValue(speech.text, out action))
        {
            action.Invoke();
            //keywordAction.Invoke();
        }
       
    }

    private void TriggerJump() {
        if(!isBirdDead()) {
            Jump();
        }
    }
}

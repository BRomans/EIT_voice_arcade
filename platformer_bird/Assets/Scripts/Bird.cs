using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;


public class Bird : MonoBehaviour
{
    private bool isDead = false;
    private Rigidbody2D rb2d;
    private Animator anim;

    public Rigidbody2D bullet;
    public float bulletSpeed = 8f;

    public float upForce = 200f;
    
    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    public Text volumeText;

    /* Initialises the player object. Called before the first frame update */
    void Start()
    {
        setupActions();
        setupRecognizer();
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        
    }

    /* Updates the player object. Called once per frame */
    void Update()
    {
        if (Input.GetMouseButtonDown (0))
        {
            Jump();
        }

        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    /* Triggers a vertical jump and the flapping animation */
    public void Jump()
    {
        if (!isDead) {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(new Vector2(0, upForce));
            anim.SetTrigger("Flap");
        }
    }

    /* Kills the player object and sets it to dead state */
    public void Die() {
        if(!isDead) {
            rb2d.velocity = Vector2.zero;
            isDead = true;
            anim.SetTrigger("Die");
            GameController.instance.birdDied();
        }
    }

    /* Generates a new bullet object */
    private void Shoot() {
        var bulletInst = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector2(0, 0)));
            bulletInst.velocity = new Vector2(bulletSpeed, 0);
    }

    /* This function triggers a restart of the game */
    private void Restart() {
        if(isDead) {
            GameController.instance.restartGame();
        }
    }

    /* This function manages 2D collisions of player object */
    private void OnCollisionEnter2D()
    {
        rb2d.velocity = Vector2.zero;
        isDead = true;
        anim.SetTrigger("Die");
        GameController.instance.birdDied();
    }

    /* Returns true if the player object is dead */
    public bool isBirdDead()  {
        return isDead;
    }


    /* This function intialises the voice controls array */
    private void setupActions() {
        actions.Add("jump", () => {
            Debug.Log("Bird is jumping");
            Jump();
        });
        actions.Add("start", () => {
            //TriggerJump()
            Debug.Log("Game has started");
            Restart();
        });
        
        actions.Add("die", () => {
            Debug.Log("You psycho!");
            Die();
        });

        actions.Add("shoot", () => {
            Debug.Log("Pew Pew!");
            Shoot();
        });

    }

    /* This function initialise the voice recognizer */
    private void setupRecognizer() {
        keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
        keywordRecognizer.Start();
        Debug.Log("Voice recognizer ready!");

    }

    /* This function triggers the voice control if it's in our dictionary */
    private void RecognizedSpeech(PhraseRecognizedEventArgs speech) {
        Debug.Log(speech.text);
        System.Action action;
        // if the keyword recognized is in our dictionary, call that Action.
        if (actions.TryGetValue(speech.text, out action))
        {
            action.Invoke();
        }
    }
}

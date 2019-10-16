using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public float jumpPower = 20.0f;
    public float movPower = 5.0f;
    Rigidbody2D myRigidbody;
    bool isGrounded = false;
    float posX = 0.0f;
    bool isGameOver = false;

    public Rigidbody2D bullet;
    public float bulletSpeed = 10f;

    private bool isDead = false;
    private Animator anim;

    private KeywordRecognizer keywordRecognizer;
    private Dictionary<string, Action> actions = new Dictionary<string, Action>();

    public Text volumeText;

    void Start() {
        setupActions();
        setupRecognizer();
        anim = GetComponent<Animator>();
        myRigidbody = transform.GetComponent<Rigidbody2D>();
        myRigidbody.AddForce(Vector3.forward * (movPower * myRigidbody.mass * 20.0f));
    }

    /* Updates the player object. Called once per frame */
    void Update() {
        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    /* Generates a new bullet object */
    private void Shoot() {
        var bulletInst = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector2(0, 0)));
        bulletInst.velocity = new Vector2(bulletSpeed, 0);
    }

    /* Triggers a vertical jump and the player animation */
    public void Jump() {
        if (isGrounded && !isGameOver)
        {
            myRigidbody.AddForce(Vector3.up * (jumpPower * myRigidbody.mass * myRigidbody.gravityScale * 20.0f));
            //myAudioPlayer.PlayOneShot(jump);
            isGrounded = false;
            //anim.SetTrigger("Flap");

        }
    }

    /* Kills the player object and sets it to dead state */
    public void Die() {
        if(!isDead) {
            myRigidbody.velocity = Vector2.zero;
            isDead = true;
            anim.SetTrigger("Die");
            GameController.instance.birdDied();
        }
    }

    /* This function triggers a restart of the game */
    private void Restart() {
        if(isDead) {
            GameController.instance.restartGame();
        }
    }

    public bool isBirdDead()  {
        return isDead;
    }

    /* This function manages 2D collisions of player object */
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag == "Ground")
        {
            anim.SetTrigger("Jump_F");
        }
        if (other.gameObject.tag == "Colum")
        {
            myRigidbody.velocity = Vector2.zero;
            isDead = true;
            anim.SetTrigger("Die");
            GameController.instance.birdDied();
        }



    }

    void OnCollisionStay2D(Collision2D other) {

        if (other.collider.tag == "Ground")
        {
            isGrounded = true;
        }

    }

    void OnCollisionExit2D(Collision2D other) {

        if (other.collider.tag == "Ground")
        {
            isGrounded = false;
            anim.SetTrigger("Jump");
        }

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Star")
        {
            //myGameController.IncrementScore();
            //myAudioPlayer.PlayOneShot(scoreSFX);
            Destroy(other.gameObject);
        }
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

        actions.Add("bang", () => {
            Debug.Log("Pew Pew!");
            Shoot();
        });

        actions.Add("peu", () => {
            Debug.Log("Pew Pew!");
            Shoot();
        });

        actions.Add("pew", () => {
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

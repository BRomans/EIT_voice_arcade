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
        if (Input.GetMouseButtonDown (0))
        {
            Jump();
        }

        if (Input.GetButtonDown("Fire1") || Input.GetKeyDown(KeyCode.Space))
        {
            var bulletInst = Instantiate(bullet, transform.position, Quaternion.Euler(new Vector2(0, 0)));
            bulletInst.velocity = new Vector2(bulletSpeed, 0);
        }
    }

    public void Jump()
    {
        if (!isDead) {
            rb2d.velocity = Vector2.zero;
            rb2d.AddForce(new Vector2(0, upForce));
            anim.SetTrigger("Flap");
        }
    }

    public void Die() {
        if(!isDead) {
            rb2d.velocity = Vector2.zero;
            isDead = true;
            anim.SetTrigger("Die");
            GameController.instance.birdDied();
        }
    }

    private void Restart() {
        if(isDead) {
            GameController.instance.restartGame();
        }
    }

    private void OnCollisionEnter2D()
    {
        rb2d.velocity = Vector2.zero;
        isDead = true;
        anim.SetTrigger("Die");
        GameController.instance.birdDied();
    }

    public bool isBirdDead()  {
        return isDead;
    }

     // voice recognition implementation

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
}

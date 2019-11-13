using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows.Speech;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float jumpPower = 2.0f;
    public float movPower = 5.0f;
    Rigidbody2D player;
    bool isGrounded = false;
    float posX = 0.0f;

    public static PlayerController instance;
    public Text ammoText;
    private int ammo;
    private float timeSinceLastReload;

    public Rigidbody2D bullet;
    public float bulletSpeed = 40f;
    private bool isDead = false;
    public bool isFlapping = false;
    private Animator anim;
    
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } 
    }

    /* Setup voice recognition, animations and the player size and movement */
    void Start() {
        anim = GetComponent<Animator>();
        player = transform.GetComponent<Rigidbody2D>();
        player.AddForce(Vector3.forward * (movPower * player.mass * 20.0f));
    }

    /* Updates the player object with commands received from the user/audio control */
    void Update() {
        if (!isDead)
        {
            // Shoot on spacebar, for debugging on Mac
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Shoot();
            }

            // Jumping with keyboard for quiet testing
            if (Input.GetKeyDown(KeyCode.J))
            {
                isFlapping = true;
                Jump();
            }
            if (Input.GetKeyUp(KeyCode.J))
            {
                isFlapping = false;
            }

            if (isFlapping)
            {
                Jump();
            }

            // Update ammo count - add bullet every 2 seconds
            updateAmmo();
            ammoText.text = "Ammo: " + ammo.ToString();
        }
    }

    /* Check if have ammo then make a new bullet and shoot it */
    public void Shoot() {
        if (ammo > 0)
        {
            var bulletInst = Instantiate(bullet, new Vector3(transform.position.x + 1, transform.position.y, transform.position.z), Quaternion.Euler(new Vector2(0, 0)));
            bulletInst.velocity = new Vector2(bulletSpeed, 0);
            ammo--;
        }
    }

    /* Add one more bullet to the ammo after a certain amount of time elapses */
    private void updateAmmo()
    {
        timeSinceLastReload += Time.deltaTime;

        if (!GameController.instance.gameOver && timeSinceLastReload >= 2) // new bullet every 2 second(s)
        {
            timeSinceLastReload = 0;
            ammo++;
        }

        // Set ammo text color to red when ammo empty, otherwise white
        if (ammo == 0)
        {
            ammoText.color = Color.red;
        } else
        {
            ammoText.color = Color.white;
        }
    }

    /* Trigger a vertical jump and play the player animation */
    public void Jump() {
        if (!isDead)
        {
            player.AddForce(Vector3.up * (jumpPower * player.mass * player.gravityScale *2f));
            //myAudioPlayer.PlayOneShot(jump);
            isGrounded = false;
            //anim.SetTrigger("Flap");
        }
    }

    /* Kill the player object, set it to dead and notify the game controller */
    public void Die() {
        if(!isDead) {
            player.velocity = Vector2.zero;
            isDead = true;
            anim.SetTrigger("Die");
            GameController.instance.birdDied();
        }
    }

    /* Trigger a restart of the game */
    public void Restart() {
        if(isDead) {
            GameController.instance.restartGame();
        }
    }

    /* Allow controller to check if bird is dead */
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
            player.velocity = Vector2.zero;
            isDead = true;
            anim.SetTrigger("Die");
            GameController.instance.birdDied();
        }
        if (other.gameObject.tag == "Roof")
        {
            Debug.Log("HIT THE ROOF BABY");
        }



    }

    /* Update the status of the player if it is on the ground to track jumping */
    void OnCollisionStay2D(Collision2D other) {

        if (other.collider.tag == "Ground")
        {
            isGrounded = true;
        }

    }

    /* Update the status of the player when it jumps ot trigger the corresponding animation */
    void OnCollisionExit2D(Collision2D other) {

        if (other.collider.tag == "Ground")
        {
            isGrounded = false;
            anim.SetTrigger("Jump");
        }

    }

    /* Destroy star objects when they collide with the player */
    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Star")
        {
            //myGameController.IncrementScore();
            //myAudioPlayer.PlayOneShot(scoreSFX);
            Destroy(other.gameObject);
        }
    }
}

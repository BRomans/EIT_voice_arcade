﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float jumpPower = 10.0f;
    public float movPower = 5.0f;
    Rigidbody2D myRigidbody;
    bool isGrounded = false;
    float posX = 0.0f;
    bool isGameOver = false;
    // Start is called before the first frame update

    void Start()
    {
      myRigidbody = transform.GetComponent<Rigidbody2D>();
      myRigidbody.AddForce(Vector3.forward * (movPower * myRigidbody.mass * 20.0f));
    }

    //

    void Update()
    {
      if (Input.GetKey(KeyCode.Space)) {
            Jump();
      }
    }

    public void Jump()
    {
        if (isGrounded && !isGameOver)
        {
            myRigidbody.AddForce(Vector3.up * (jumpPower * myRigidbody.mass * myRigidbody.gravityScale * 20.0f));
            //myAudioPlayer.PlayOneShot(jump);
            isGrounded = false;
        }
    }

    void OnCollisionStay2D(Collision2D other)
    {

        if (other.collider.tag == "Ground")
        {
            isGrounded = true;
        }

    }


    void OnCollisionExit2D(Collision2D other)
    {

        if (other.collider.tag == "Ground")
        {
            isGrounded = false;
        }

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Star") {
            //myGameController.IncrementScore();
            //myAudioPlayer.PlayOneShot(scoreSFX);
            Destroy(other.gameObject);
        }
    }
}
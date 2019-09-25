﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bird : MonoBehaviour
{
    private bool isDead = false;
    private Rigidbody2D rb2d;
    private Animator anim;

    public float upForce = 200f;

    public Text volumeText;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (Input.GetMouseButtonDown (0))
            {
                // reset velocity to zero so behavior stays consistent
                rb2d.velocity = Vector2.zero;

                rb2d.AddForce(new Vector2(0, upForce));
                anim.SetTrigger("Flap");
            } else if (volumeText.text == "volume flap")
            {
                rb2d.velocity = Vector2.zero;

                rb2d.AddForce(new Vector2(0, upForce));
                anim.SetTrigger("Flap");
            }
        }
    }

    //public void Flap()
    //{
    //    if (!isDead)
    //    {
    //        rb2d.velocity = Vector2.zero;

    //        rb2d.AddForce(new Vector2(0, upForce));
    //        anim.SetTrigger("Flap");
    //    }
    //}

    private void OnCollisionEnter2D()
    {
        rb2d.velocity = Vector2.zero;
        isDead = true;
        anim.SetTrigger("Die");
        GameController.instance.birdDied();
    }
}

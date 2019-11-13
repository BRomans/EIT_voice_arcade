  ﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Obstacle class */
public class Asteroid : MonoBehaviour
{
    /* Tell controller the player scored when bird passes object */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
          {
              //do nothing for the moment
          }
    }

    /* Destroy lasers on contact with obstacles */
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "laser_0(Clone)")
        {
            Destroy(col.gameObject);
        }
    }
}

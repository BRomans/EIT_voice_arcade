  ﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Obstacle class */
public class Column : MonoBehaviour
{
    /* Tell controller the player scored when bird passes object */
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
          {
              GameController.instance.birdScored();
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

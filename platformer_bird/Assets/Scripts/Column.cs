  ﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
          {
              GameController.instance.birdScored();
          }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "laser_0(Clone)")
        {
            Destroy(col.gameObject);
        }
    }
}

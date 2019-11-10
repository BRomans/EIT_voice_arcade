using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class for objects that can be destroyed by player's bullets */
public class Shootable : MonoBehaviour
{
    void Start() { }

    void Update() { }

    /* Define behavior for various collision types */
    void OnCollisionEnter2D(Collision2D col)
    {
        // Shootable by laser bullet
        if (col.gameObject.name == "laser_0(Clone)")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
            GameController.instance.birdScored(2); // shootable objects worth 2 points
        }

        // Prevent spawning on top of obstacles
        if (col.gameObject.name == "AstroidNEW 1(Clone)")
        {
            Destroy(gameObject);
        }

        // Destroy when hit player and game ends
        if (col.gameObject.name == "Player")
        {
            Destroy(gameObject);
            GameController.instance.alienAttacked();
        }
    }
}

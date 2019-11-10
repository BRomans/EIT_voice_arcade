using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Define scrolling behavior for platformer-style animation */
public class ScrollingObject : MonoBehaviour
{
    private Rigidbody2D rb2d;
    
    /* Give a component the scrollSpeed defined for the entire game */
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.velocity = new Vector2(GameController.instance.scrollSpeed, 0);
    }

    /* Stop scrolling if game ends */
    void Update()
    {
        if (GameController.instance.gameOver)
        {
            rb2d.velocity = Vector2.zero;
        } else
        {
            // ensure component continues moving at full speed (combat any friction)
            rb2d.velocity = new Vector2(GameController.instance.scrollSpeed, 0);
        }
    }
}

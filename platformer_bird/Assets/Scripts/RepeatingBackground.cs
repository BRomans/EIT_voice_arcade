using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Repeat the background to give the scrolling effect with only two versions of the background image */
public class RepeatingBackground : MonoBehaviour
{
    private BoxCollider2D groundCollider;
    private float groundHorizontalLength;

    /* Setup up colliders */
    void Start()
    {
        groundCollider = GetComponent<BoxCollider2D>();
        groundHorizontalLength = groundCollider.size.x;
    }

    /* Check if a background image has completely let the screen */
    void Update()
    {
        if (transform.position.x < -groundHorizontalLength)
        {
            repositionBG();
        }
    }

    /* Reposition the background offscreen in front of the player */
    private void repositionBG()
    {
        Vector2 groundOffset = new Vector2(groundHorizontalLength * 2f, 0);
        transform.position = (Vector2)transform.position + groundOffset;
    }
}

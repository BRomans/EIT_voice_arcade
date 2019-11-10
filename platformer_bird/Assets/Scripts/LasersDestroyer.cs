using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Memory manager class to destroy spawned objects that escape the game (lasers and aliens) */
public class LasersDestroyer : MonoBehaviour
{
    void Start() {}

    void Update() { }

    /* Destroy any colliding objects */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}

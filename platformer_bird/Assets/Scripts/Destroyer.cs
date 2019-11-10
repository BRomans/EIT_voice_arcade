using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    public string toBeDestroyed;

    void Start() { }

    void Update() { }

    /* Destroy specific colliding objects with correct name */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == toBeDestroyed)
        {
            Destroy(collision.gameObject);
        }
    }
}

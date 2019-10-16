using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shootable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("collision name = " + col.gameObject.name);

        if (col.gameObject.name == "laser_0(Clone)")
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }

        if (col.gameObject.name == "AstroidNEW(Clone)")
        {
            Destroy(gameObject);
        }

        if (col.gameObject.name == "Bird")
        {
            Destroy(gameObject);
        }
    }
}

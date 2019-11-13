using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Using pooling to potimize memory for creating the obstacles */
public class AsteroidsPool : MonoBehaviour
{
    public int columnPoolSize = 3;
    public GameObject columnPrefab;
    public float spawnRate = 8f;
    public float columnMin = 0f;
    public float columnMax = -1f;
    public float columnYsetPosition = 10f;

    private GameObject[] columns; // column pool
    private Vector2 objectPoolPosition = new Vector2(-15f, -25f); // offscreen position for holding unused columns
    private float timeSinceLastSpawn;
    private float spawnXPosition = 20f;
    private int currentColumn = 0;

    /* Initialize a pool of obstacles (columns) of size columnPoolSize off screen */
    void Start()
    {
        columns = new GameObject[columnPoolSize];

        for (int i=0; i<columnPoolSize; i++)
        {
            columns[i] = (GameObject)Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);
        }
    }

    /* Move the next obstacle in the pool to the correct position in front of the player */
    void Update()
    {
        if (GameController.instance.gameStarted)
        {
            timeSinceLastSpawn += Time.deltaTime;

            if (!GameController.instance.gameOver && timeSinceLastSpawn >= spawnRate)
            {
                timeSinceLastSpawn = 0;
                float spawnYPosition = Random.Range(2.0f, 6.0f);

                // Randomly resize the next obstacle to increase difficulty and add dynamism
                float scaleX = Random.Range(0.2f, 0.8f);
                float scaleY = scaleX;
                columns[currentColumn].transform.localScale = new Vector2(scaleX, scaleY);

                // Place the next obstacle
                columns[currentColumn].transform.position = new Vector2(spawnXPosition, spawnYPosition);
                currentColumn++;
                if (currentColumn >= columnPoolSize)
                {
                    currentColumn = 0;
                }
            }
        }
    }
}

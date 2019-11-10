using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColumnPool : MonoBehaviour
{
    public int columnPoolSize = 5;
    public GameObject columnPrefab;
    public float spawnRate = 4f;
    public float columnMin = 0f;//-1f;
    public float columnMax = -1f;//3.5f;
    public float columnYsetPosition = 0f;

    private GameObject[] columns;
    private Vector2 objectPoolPosition = new Vector2(-15f, -25f);
    private float timeSinceLastSpawn;
    private float spawnXPosition = 10f;
    private int currentColumn = 0;

    // Start initializes the pool of obstacles (columns)
    void Start()
    {
        columns = new GameObject[columnPoolSize];

        for (int i=0; i<columnPoolSize; i++)
        {
            columns[i] = (GameObject)Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);
        }
    }

    // Update moves the next obstacle to the correct position in front of the player
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (!GameController.instance.gameOver && timeSinceLastSpawn >= spawnRate)
        {
            timeSinceLastSpawn = 0;
            float spawnYPosition = -1.8f;

            // TODO: random resize of obstacles to increase difficulty and make it interesting
            float scaleX = Random.Range(1, 2);
            float scaleY = scaleX * 1.4f;
            columns[currentColumn].transform.localScale = new Vector2(scaleX, scaleY);

            columns[currentColumn].transform.position = new Vector2(spawnXPosition, spawnYPosition);
            currentColumn++;
            if (currentColumn >= columnPoolSize)
            {
                currentColumn = 0;
            }
        }
    }
}

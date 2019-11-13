using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Using pooling to optimize memory for creating the obstacles */
public class AsteroidPool : MonoBehaviour
{
    public int asteroidPoolSize = 5;
    public GameObject asteroidPrefab;
    public float spawnRate = 4f;
    public float asteroidMin = 0f;
    public float asteroidMax = -1f;
    public float asteroidYsetPosition = 0f;

    private GameObject[] asteroids; // asteroid pool
    private Vector2 objectPoolPosition = new Vector2(-15f, -25f); // offscreen position for holding unused asteroids
    private float timeSinceLastSpawn;
    private float spawnXPosition = 10f;
    private int currentAsteroid = 0;

    /* Initialize a pool of obstacles (asteroids) of size asteroidPoolSize off screen */
    void Start()
    {
        if(asteroidPrefab == null) {
            Debug.Log("Something went wrong when loading the asteroid, making a new one", this);
            asteroidPrefab = (GameObject)Instantiate(Resources.Load("asteroidPrefab"));
        }
        asteroids = new GameObject[asteroidPoolSize];

        for (int i=0; i<asteroidPoolSize; i++)
        {
            asteroids[i] = (GameObject)Instantiate(asteroidPrefab, objectPoolPosition, Quaternion.identity);
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
                float spawnYPosition = -1.8f;

                // Randomly resize the next obstacle to increase difficulty and add dynamism
                float scaleX = Random.Range(1, 2);
                float scaleY = scaleX * 1.4f;
                asteroids[currentAsteroid].transform.localScale = new Vector2(scaleX, scaleY);

                // Place the next obstacle
                asteroids[currentAsteroid].transform.position = new Vector2(spawnXPosition, spawnYPosition);
                currentAsteroid++;
                if (currentAsteroid >= asteroidPoolSize)
                {
                    currentAsteroid = 0;
                }
            }
        }
    }
}

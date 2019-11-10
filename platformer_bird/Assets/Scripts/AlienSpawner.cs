using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime = 5f;

    private float timeSinceLastSpawn;
    private float spawnRateMin = 5f;
    private float spawnRateMax = 12f;
    private float alienYsetPosistion = -1.6f;
    private float randomXMin = 5f;
    private float randomXMax = 12f;

    void Start() {}

    /* Check if it's time to spawn again, then respawn a new alien */
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        // Add randomness to spawn rate
        float spawnRate = Random.Range(spawnRateMin, spawnRateMax);

        // Call spawn function if time to create new alien
        if (!GameController.instance.gameOver && timeSinceLastSpawn >= spawnRate)
        {
            // Reset spawn timer
            timeSinceLastSpawn = 0;

            spawn();
        }
    }

    /* Spawn a new alien */
    private void spawn()
    {
        // Add randomness to spawn location if needed
        float spawnXPosition = Random.Range(randomXMin, randomXMax);

        Instantiate(enemy, new Vector3(10, alienYsetPosistion, 0), Quaternion.identity);
    }
}

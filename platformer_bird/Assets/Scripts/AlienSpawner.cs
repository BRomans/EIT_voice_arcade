using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienSpawner : MonoBehaviour
{
    public GameObject enemy;
    public float spawnTime = 5f;

    private float timeSinceLastSpawn;
    private float spawnRate = 4f;
    private float spawnRateMin = 5f;
    private float spawnRateMax = 12f;
    private float alienYsetPosistion = -1.6f;
    private float randomXMin = 5f;
    private float randomXMax = 12f;

    // Start is called before the first frame update
    void Start()
    {
        //InvokeRepeating("Spawn", spawnTime, spawnTime);
    }

    //void Spawn()
    //{
    //    if (GameController.instance.gameOver)
    //    {
    //        // ... exit the function.
    //        return;
    //    }

    //    // Create an instance of the enemy prefab at the specified position.
    //    Instantiate(enemy, new Vector3(10, alienYsetPosistion, 0), Quaternion.identity);

    //}

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        // make spawn rate random
        spawnRate = Random.Range(spawnRateMin, spawnRateMax);

        if (!GameController.instance.gameOver && timeSinceLastSpawn >= spawnRate)
        {
            timeSinceLastSpawn = 0;

            float spawnXPosition = Random.Range(randomXMin, randomXMax);

            Instantiate(enemy, new Vector3(10, alienYsetPosistion, 0), Quaternion.identity);
        }
    }
}

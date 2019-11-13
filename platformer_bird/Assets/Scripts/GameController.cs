﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject gameOverText;
    public GameObject scoreText2;
    public GameObject startText;
    public bool gameOver = false;
    public bool gameStarted = false;
    public float scrollSpeed = -1.5f;
    public Text scoreText;

    public Dropdown micSensitivitySelector;
    public string volumeSensitivity = "med"; // or "low" or "high"

    public GameObject bird;

    private int score = 0;
    public bool flapping;

    /* Initiate instance of the game, destroy any other instances */
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }

        // Set default sensitivy to medium
        micSensitivitySelector.value = 1;
    }

    /* Restart if needed, then check the bird's status */
    void Update()
    {
        if (gameOver && Input.GetMouseButtonDown(0))
        {
            restartGame();
        }

        if (gameStarted)
        {
            updateVolumeSensitivity();
            startText.SetActive(false);
            scoreText2.SetActive(true);
            bird.GetComponent<PlayerController>().isFlapping = flapping;
        } else if (Input.GetKeyDown(KeyCode.Space))
        {
            gameStarted = true;
        }
    }

    /* Update the score and scoreboard when the player scores, with i points */
    public void birdScored(int i)
    {
        if (gameOver)
        {
            return;
        }

        score += i;
        scoreText.text = "Score: " + score.ToString();

        // Check if difficulty should increase based on new score
        updateDifficulty();
    }

    /* Lose two points when alien attacks */
    public void alienAttacked()
    {
        if (gameOver)
        {
            return;
        }

        score -= 2;
        scoreText.text = "Score: " + score.ToString();

        // Check if difficulty should increase based on new score
        updateDifficulty();
    }

    /* Increase the difficulty (speed) as the score gets higher */
    private void updateDifficulty()
    {
        // Current rate: 0.02 speed added for every 5 points
        float factor = 0.2f * (score % 5);
        scrollSpeed -= factor;
    }

    /* Set the volume sensitivy - at the beginning of the game */
    private void updateVolumeSensitivity()
    {
        volumeSensitivity = micSensitivitySelector.options[micSensitivitySelector.value].text;
    }

    /* Setup all game over attributes when bird dies */
    public void birdDied()
    {
        gameOverText.SetActive(true);
        scoreText2.SetActive(false);
        gameOver = true;
    }

    /* Reload the scene for restarting */
    public void restartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /* Tell the bird player that it is flapping now
     - relaying information from the audio visualizer, to the player */
    public void birdFlap()
    {
        bird.GetComponent<PlayerController>().isFlapping = true;
    }

    /* Tell the bird player that it is not flapping now
    - relaying information from the audio visualizer, to the player */
    public void notFlapping()
    {
        bird.GetComponent<PlayerController>().isFlapping = false;
    }
}

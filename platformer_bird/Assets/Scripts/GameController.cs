using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public GameObject gameOverText;
    public bool gameOver = false;
    public float scrollSpeed = -1.5f;
    public Text scoreText;

    public GameObject bird;

    private int score = 0;
    public bool flapping;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver && Input.GetMouseButtonDown(0))
        {
            restartGame();
        }

        bird.GetComponent<PlayerScript>().isFlapping = flapping;
    }

    public void birdScored()
    {
        if (gameOver)
        {
            return;
        }

        score++;
        scoreText.text = "Score: " + score.ToString();

        // increase speed as score gets higher
        // current rate: .2 speed every 5 points
        float factor = 0.2f * (score % 5);
        scrollSpeed -= factor;
    }

    public void birdDied()
    {
        gameOverText.SetActive(true);
        gameOver = true;
    }

    public void restartGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void birdFlap()
    {
        bird.GetComponent<PlayerScript>().isFlapping = true;
    }

    public void notFlapping()
    {
        bird.GetComponent<PlayerScript>().isFlapping = false;
    }
}

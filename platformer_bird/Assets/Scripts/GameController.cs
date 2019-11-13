using System.Collections;
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

    public int volumeSensitivity = 1; // or 0 or 2

    public GameObject bird;

    private int score = 0;
    public bool flapping;

    public Dropdown micDropdown;

    private float timeAtStart;
    VoiceRecognitionController voiceRecognitionController;

    // Difficulty increase rate: +0.2 speed every 5 seconds
    float difficultyFactor = 0.2f;
    float difficultyDelay = 5f;

    /* Initiate instance of the game, destroy any other instances */
    void Awake()
    {
        micDropdown.value = 1;

        if (instance == null)
        {
            instance = this;
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        voiceRecognitionController = new VoiceRecognitionController();
        micDropdown.value = 1;
        micDropdown.onValueChanged.AddListener(delegate {
            updateVolumeSensitivity();
        });
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
            updateDifficulty();
            micDropdown.enabled = false;
            startText.SetActive(false);
            scoreText2.SetActive(true);
            bird.GetComponent<PlayerController>().isFlapping = flapping;
        } else if (Input.GetKeyDown(KeyCode.Space))
        {
            startGame();
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
    }

    /* Increase the difficulty (speed) as the score gets higher */
    private void updateDifficulty()
    {
        if (Time.time >= timeAtStart + difficultyDelay)
        {
            scrollSpeed -= difficultyFactor;
            timeAtStart = Time.time;
        }
    }

    /* Set the volume sensitivy - at the beginning of the game */
    private void updateVolumeSensitivity()
    {
        volumeSensitivity = micDropdown.value;
        Debug.Log("Sensitivity: " + volumeSensitivity);
    }

    /* Set the volume sensitivy - using voice command */
    public void updateVolumeSensitivity(int val)
    {
        volumeSensitivity = val;
        micDropdown.value = val;
        Debug.Log("Sensitivity: " + volumeSensitivity);
    }

    /* Setup all game over attributes when bird dies */
    public void birdDied()
    {
        micDropdown.enabled = true;
        gameOverText.SetActive(true);
        scoreText2.SetActive(false);
        gameOver = true;
    }

    public void startGame() {
        gameStarted = true;
        timeAtStart = Time.time;
    }

    /* Reload the scene for restarting */
    public void restartGame() {
        reloadGame();
        startGame();
    }

    public void reloadGame() {
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

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

    public float difficultyRate = 5f; // how often to add difficulty (in seconds)
    public float difficultyFactor = 0.2f; // amount of speed to add for difficulty

    public Dropdown micSensitivitySelector;
    public string volumeSensitivity = "Medium"; // or "Low" or "High"

    public GameObject bird;

    private int score = 0;
    public bool flapping;
    private float timeAtStart;
    VoiceRecognitionController voiceRecognitionController;

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
    }

    void Start()
    {
        voiceRecognitionController = new VoiceRecognitionController();
        voiceRecognitionController.setupActions();
        voiceRecognitionController.setupRecognizer();
        micSensitivitySelector.value = 1;
        micSensitivitySelector.onValueChanged.AddListener(delegate {
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
            startText.SetActive(false);
            scoreText2.SetActive(true);
            micSensitivitySelector.enabled = false;
            bird.GetComponent<PlayerController>().isFlapping = flapping;
            updateDifficulty();
        } else if (Input.GetKeyDown(KeyCode.Space))
        {
            
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
        if (Time.time >= timeAtStart + difficultyRate)
        {
            scrollSpeed -= difficultyFactor;
            timeAtStart = Time.time;
        }
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
        micSensitivitySelector.enabled = true;
        gameOver = true;
    }

    public void startGame() {
        gameStarted = true;
        timeAtStart = Time.time;
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

    public PlayerController GetPlayer() {
        return bird.GetComponent<PlayerController>();
    }
}

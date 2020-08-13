using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject titleScreen;
    public Button togglePauseButton;
    public List<GameObject> targetList;
    public float spawnRate = 1.0f;

    [Header("Score Settings")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    private int score;

    public Button restartButton;

    public bool isGameOver;

    private AudioSource audioSource;
    public AudioClip scoreSound; //on making score

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>();

        //StartGame(); //The game should be started via the Title screen

        togglePauseButton.onClick.AddListener(OnTogglePause);
    }

    private void OnTogglePause()
    {
        const float pausedTimeScale = 0f;
        const float resumedTimeScale = 1.0f;

        var isGamePaused = Time.timeScale < resumedTimeScale;

        Time.timeScale = isGamePaused ? resumedTimeScale : pausedTimeScale;

        if (Time.timeScale < resumedTimeScale)
        {
            audioSource.Pause();
        }
        else
        {
            audioSource.Play();
        }
    }

    public void StartGame(Difficulty difficultyLevel)
    {
        //set difficulty level
        spawnRate /= (int)difficultyLevel;
        Debug.Log("Spawn Rate: " + spawnRate);

        isGameOver = false; //set to default each time game starts

        StartCoroutine(nameof(SpawnTarget));

        UpdateScore(0);

        //hide the title screen
        titleScreen.gameObject.SetActive(false);
        audioSource.Play();
    }

    private IEnumerator SpawnTarget()
    {
        //Debug.Log(targetList.Count);

        while (!isGameOver) //if game is not over, spawn targets
        {
            yield return new WaitForSeconds(spawnRate);

            var index = Random.Range(0, targetList.Count);

            var target = targetList[index];
            Instantiate(target, target.transform.position, target.transform.rotation); //instantiate random target            
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        //Set score
        score += scoreToAdd;

        //display score
        scoreText.text = $"Score: {score}";

        //On making a score, play the score sound
        audioSource.PlayOneShot(scoreSound, 1);
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);
        isGameOver = true;

        audioSource.Stop();
    }

    public void RestartGame()
    {
        //Reloads the currently active scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

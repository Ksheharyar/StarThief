using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Star Settings")]
    public GameObject starPrefab;
    public int maxStars = 5;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;

    [Header("Spawn Area")]
    public float spawnX = 5f;
    public float spawnY = 2.5f;

    [Header("Audio")]
    public AudioClip collectSound;

    private int score = 0;

    private List<GameObject> starPool = new List<GameObject>();

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        CreatePool();

        SpawnInitialStars();

        UpdateScore();

        gameOverPanel.SetActive(false);
    }

    void CreatePool()
    {
        for (int i = 0; i < maxStars; i++)
        {
            GameObject star = Instantiate(starPrefab);
            star.SetActive(false);
            starPool.Add(star);
        }
    }

    void SpawnInitialStars()
    {
        for (int i = 0; i < maxStars; i++)
        {
            ActivateStar();
        }
    }

    void ActivateStar()
    {
        foreach (GameObject star in starPool)
        {
            if (!star.activeInHierarchy)
            {
                star.transform.position = GetRandomPosition();
                star.SetActive(true);
                return;
            }
        }
    }

    Vector2 GetRandomPosition()
    {
        return new Vector2(
            Random.Range(-spawnX, spawnX),
            Random.Range(-spawnY, spawnY)
        );
    }

    public void AddScore(GameObject collectedStar)
    {
        score++;

        UpdateScore();

        // play collect sound
        if (collectSound != null)
        {
            audioSource.PlayOneShot(collectSound);
        }

        // disable collected star (pooling)
        collectedStar.SetActive(false);

        ActivateStar();
    }

    void UpdateScore()
    {
        scoreText.text = "Score: " + score;
    }

    public void GameOver()
    {
        Debug.Log("GAME OVER");

        Time.timeScale = 0f;

        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("Main_menu");
    }

    public void ExitGame()
    {
        Application.Quit();

        Debug.Log("Exit pressed");
    }
}
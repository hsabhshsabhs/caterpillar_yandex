using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score = 0;

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Game")
        {
            scoreText = GameObject.Find("ScoreText")?.GetComponent<TextMeshProUGUI>();

            if (scoreText != null)
            {
                scoreText.gameObject.SetActive(true);
                score = 0;
                UpdateScoreText();
            }
            else
            {
                Debug.LogWarning("Не удалось найти 'ScoreText' на игровой сцене!");
            }
        }
        else
        {
            if (scoreText != null)
            {
                scoreText.gameObject.SetActive(false);
            }
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Счет: " + score;
        }
    }

    public void GameOver()
    {
        Debug.Log("Игра окончена! Возвращаемся в главное меню.");
        SceneManager.LoadScene("MainMenu");
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
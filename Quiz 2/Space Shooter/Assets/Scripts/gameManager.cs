using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class gameManager : MonoBehaviour
{
    public static gameManager Instance;

    public int score = 0;
    public TMP_Text scoreText;
    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;

    void Awake()
    {
        Instance = this;
        gameOverPanel.SetActive(false);
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;
    }

    public void EndGame()
    {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        finalScoreText.text = "Final Score: " + score;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

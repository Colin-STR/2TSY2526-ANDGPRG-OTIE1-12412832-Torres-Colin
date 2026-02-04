using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class uiManager : MonoBehaviour
{
    public static uiManager Instance;

    [Header("HUD")]
    public TMP_Text scoreText;
    public Image healthBarFill;

    [Header("Game Over Screen")]
    public GameObject gameOverPanel;
    public TMP_Text finalScoreText;

    private int currentScore = 0;
    private int lastMilestone = 0;

    void Awake() => Instance = this;

    public void UpdateScore(int points)
    {
        currentScore += points;
        scoreText.text = "Score: " + currentScore;
        if (currentScore >= lastMilestone + 500)
        {
            if (audioManager.Instance != null)
            {
                audioManager.Instance.PlaySFX(audioManager.Instance.milestoneSFX);
            }
            lastMilestone = (currentScore / 500) * 500;
        }
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
        finalScoreText.text = "FINAL SCORE: " + currentScore;
        if (audioManager.Instance != null)
        {
            audioManager.Instance.StopMusic();
            audioManager.Instance.PlaySFX(audioManager.Instance.gameOverSFX);
        }
        Time.timeScale = 0f;
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        float fillPercentage = (float)currentHealth / (float)maxHealth;
        healthBarFill.fillAmount = fillPercentage;
    }
}

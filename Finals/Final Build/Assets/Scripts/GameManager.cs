using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Economy")]
    public int gold = 100;

    [Header("Player Stats")]
    public int coreHealth = 20;

    public GameObject gameOverPanel;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void AddGold(int amount)
    {
        gold += amount;
        Debug.Log("Gold Added! Current Gold: " + gold);
    }

    public bool TrySpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            Debug.Log("Gold Spent! Remaining: " + gold);
            return true;
        }

        Debug.Log("Not enough gold!");
        return false;
    }
    public void TakeDamage(int amount)
    {
        coreHealth -= amount;
        if (coreHealth <= 0)
        {
            coreHealth = 0;
            TriggerGameOver();
        }
    }

    void TriggerGameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (audioManager.Instance != null)
        {
            audioManager.Instance.StopMusic();
            audioManager.Instance.PlaySFX(audioManager.Instance.gameOverSFX);
        }
    }
    public void RestartGame()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitToDesktop()
    {
        Application.Quit();
        Debug.Log("Game Exited");
    }
}
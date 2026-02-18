using UnityEngine;
using System.Collections;

public class WaveManager : MonoBehaviour
{
    public enum Level { Easy, Medium, Hard, Victory }
    public Level currentLevel = Level.Easy;

    [Header("Enemy Prefabs")]
    public GameObject normalPrefab;
    public GameObject fastPrefab;
    public GameObject slowPrefab;

    [Header("Settings")]
    public Transform[] spawnPoints;
    public float spawnRate = 2f;
    private float nextSpawnTime;

    [Header("Wave Logic")]
    public int killsRequiredPerLevel = 20;
    public int maxEnemiesAtOnce = 20;
    public float gracePeriod = 5f;

    private int currentKills = 0;
    private bool isGracePeriod = false;

    private UIManager ui;

    void Start()
    {
        ui = FindObjectOfType<UIManager>();
        ui.UpdateKills(currentKills, killsRequiredPerLevel);
        AudioManager.Instance.PlayBGM(AudioManager.Instance.level1Music);
    }

    void Update()
    {
        int enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (!isGracePeriod && enemyCount < maxEnemiesAtOnce && Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        if (spawnPoints.Length == 0) return;

        Transform sp = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject toSpawn = null;

        switch (currentLevel)
        {
            case Level.Easy:
                toSpawn = normalPrefab;
                break;
            case Level.Medium:
                toSpawn = (Random.value > 0.3f) ? normalPrefab : fastPrefab;
                break;
            case Level.Hard:
                toSpawn = (Random.value > 0.5f) ? fastPrefab : slowPrefab;
                break;
            case Level.Victory:
                return;
        }

        if (toSpawn != null) Instantiate(toSpawn, sp.position, Quaternion.identity);
    }

    public void RegisterKill()
    {
        currentKills++;
        Debug.Log("Kills: " + currentKills + "/" + killsRequiredPerLevel);
        ui.UpdateKills(currentKills, killsRequiredPerLevel);

        if (currentKills >= killsRequiredPerLevel)
        {
            StartCoroutine(LevelTransition());
        }
        if (isGracePeriod)
        {
            return;
        }
    }

    IEnumerator LevelTransition()
    {
        isGracePeriod = true;
        currentKills = 0;
        AudioManager.Instance.StopBGM();

        GameObject[] remainingEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject e in remainingEnemies) Destroy(e);

        if (currentLevel == Level.Hard)
        {
            currentLevel = Level.Victory;
            ui.UpdateKills(0, 0);
            ui.ShowVictory();
            AudioManager.Instance.PlayBGM(AudioManager.Instance.victoryMusic, false);
            yield break;
        }

        if (currentLevel == Level.Easy)
        {
            currentLevel = Level.Medium;
        }
        else if (currentLevel == Level.Medium)
        {
            currentLevel = Level.Hard;
        }
        Debug.Log("Level Complete! Grace period started...");
        yield return new WaitForSeconds(gracePeriod);
        if (currentLevel == Level.Medium) AudioManager.Instance.PlayBGM(AudioManager.Instance.level2Music);
        else if (currentLevel == Level.Hard) AudioManager.Instance.PlayBGM(AudioManager.Instance.level3Music);
        isGracePeriod = false;
        ui.UpdateKills(0, killsRequiredPerLevel);
    }
}
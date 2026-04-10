using UnityEngine;
using System.Collections;
using TMPro;

public class WaveSpawner : MonoBehaviour
{
    public Transform spawnPoint;
    public TextMeshProUGUI waveText;
    public GameObject[] enemyPrefabs;

    [Header("Settings")]
    public float baseSpawnRate = 2.0f;
    public float timeBeforeFirstWave = 3.0f;

    private int waveIndex = 0;
    private bool isSpawning = false;

    void Start()
    {
        StartCoroutine(AutoWaveRoutine());
    }

    IEnumerator AutoWaveRoutine()
    {
        yield return new WaitForSeconds(timeBeforeFirstWave);

        while (waveIndex < 5)
        {
            if (!isSpawning)
            {
                yield return StartCoroutine(SpawnWave());

                while (GameObject.FindGameObjectsWithTag("Monster").Length > 0)
                {
                    yield return new WaitForSeconds(1.0f);
                }

                yield return new WaitForSeconds(5.0f);
            }
        }
    }

    IEnumerator SpawnWave()
    {
        isSpawning = true;
        waveIndex++;
        if (waveText != null) waveText.text = "Wave: " + waveIndex;

        int enemyCount = waveIndex * 3;
        float currentSpawnInterval = Mathf.Max(0.5f, baseSpawnRate - (waveIndex * 0.3f));

        for (int i = 0; i < enemyCount; i++)
        {
            SpawnEnemy(enemyPrefabs[waveIndex - 1]);
            yield return new WaitForSeconds(currentSpawnInterval);
        }

        isSpawning = false;
    }

    void SpawnEnemy(GameObject enemy)
    {
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }
}
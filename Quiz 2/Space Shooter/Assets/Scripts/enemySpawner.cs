using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnWidth = 8f;
    public int maxEnemies = 2;

    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (enemies.Length == 0)
        {
            SpawnWave();
        }
    }

    void SpawnWave()
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            float randomX = Random.Range(-spawnWidth, spawnWidth);
            Vector2 spawnPos = new Vector2(randomX, transform.position.y + (i * 1.5f));
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }
    }
}

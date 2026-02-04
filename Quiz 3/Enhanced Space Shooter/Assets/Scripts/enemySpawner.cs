using UnityEngine;

public class enemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnZ = 15f; 
    public float spawnWidth = 8f;
    public float spawnRate = 2f;

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 1f, spawnRate);
    }
    void SpawnEnemy()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < 2)
        {
            float randomX = Random.Range(-spawnWidth, spawnWidth);
            Vector3 spawnPos = new Vector3(randomX, 0f, spawnZ);
            Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        }            
    }
}

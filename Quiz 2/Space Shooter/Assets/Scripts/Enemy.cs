using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float verticalSpeed = 3f;
    public float zigzagFrequency = 2f;
    public float zigzagWidth = 4f; 

    private float spawnX;
    private float timeElapsed;

    [Header("Shooting")]
    public GameObject enemyBulletPrefab;
    public float fireRate = 1.5f;
    private float nextFireTime;

    void Start()
    {
        spawnX = transform.position.x;
    }

    void Update()
    {
        timeElapsed += Time.deltaTime;

        float newY = transform.position.y - (verticalSpeed * Time.deltaTime);

        float newX = spawnX + Mathf.Sin(timeElapsed * zigzagFrequency) * zigzagWidth;

        transform.position = new Vector2(newX, newY);

        if (transform.position.y < -10f) Destroy(gameObject);

        if (Time.time > nextFireTime)
        {
            EnemyShoot();
            nextFireTime = Time.time + fireRate + Random.Range(-0.2f, 0.2f);
        }
    }

    void EnemyShoot()
    {
        Instantiate(enemyBulletPrefab, transform.position, Quaternion.identity);
    }
}

using UnityEngine;

[System.Serializable]
public class TowerLevel
{
    public int upgradeCost;
    public float damage;
    public float range;
    public float fireRate;
}

public class Tower : MonoBehaviour
{
    [Header("Upgrade Data")]
    public TowerLevel[] levels;
    private int currentLevelIndex = 0;

    [Header("Setup")]
    public string enemyTag = "Monster";
    public GameObject projectilePrefab;
    public Transform firePoint;

    private Transform target;
    private float fireCountdown = 0f;

    public TowerLevel CurrentLevel => levels[currentLevelIndex];

    void Update()
    {
        UpdateTarget();
        if (target == null) return;
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Euler(0f, lookRotation.eulerAngles.y, 0f);
        if (fireCountdown <= 0f)
        {
            Shoot();
            fireCountdown = 1f / CurrentLevel.fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(transform.position, enemy.transform.position);
            if (distance < shortestDistance && distance <= CurrentLevel.range)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        target = (nearestEnemy != null) ? nearestEnemy.transform : null;
    }

    void Shoot()
    {
        GameObject bulletGO = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Projectile bullet = bulletGO.GetComponent<Projectile>();

        if (bullet != null)
        {
            bullet.Seek(target, CurrentLevel.damage);

            if (gameObject.name.Contains("Cannon"))
                audioManager.Instance.PlaySFX(audioManager.Instance.cannonSFX);
            else
                audioManager.Instance.PlaySFX(audioManager.Instance.crossbowSFX);
        }
    }

    public void Upgrade()
    {
        if (currentLevelIndex < levels.Length - 1)
        {
            currentLevelIndex++;
            transform.localScale *= 1.1f;
            Debug.Log("Upgraded to Level: " + (currentLevelIndex + 1));
        }
    }

    public int GetUpgradeCost()
    {
        if (currentLevelIndex < levels.Length - 1)
            return levels[currentLevelIndex + 1].upgradeCost;

        return -1;
    }

    void OnDrawGizmosSelected()
    {
        if (levels != null && levels.Length > 0)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, CurrentLevel.range);
        }
    }
}
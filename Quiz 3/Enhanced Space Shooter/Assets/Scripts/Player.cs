using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 10f;
    private Rigidbody rb;
    private Vector3 moveInput;

    [Header("Shooting")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 0.25f;
    private float nextFireTime = 0f;

    [Range(1, 3)]
    public int shotMode = 1;

    [Header("Fire Points")]
    public Transform centerPoint;
    public Transform leftPoint;
    public Transform rightPoint;

    [Header("Health")]
    public int maxHealth = 3;
    private int currentHealth;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        currentHealth = maxHealth;
    }

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        moveInput = new Vector3(moveX, 0f, moveZ).normalized;

        if (Input.GetKeyDown(KeyCode.Alpha1)) shotMode = 1;
        if (Input.GetKeyDown(KeyCode.Alpha2)) shotMode = 2;
        if (Input.GetKeyDown(KeyCode.Alpha3)) shotMode = 3;

        if (Input.GetButton("Fire1") && Time.time > nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }
    void Shoot()
    {
        if (audioManager.Instance != null)
        {
            audioManager.Instance.PlaySFX(audioManager.Instance.shootSFX);
        }
        switch (shotMode)
        {
            case 1:
                Instantiate(bulletPrefab, centerPoint.position, Quaternion.identity);
                break;
            case 2:
                Instantiate(bulletPrefab, leftPoint.position, Quaternion.identity);
                Instantiate(bulletPrefab, rightPoint.position, Quaternion.identity);
                break;
            case 3:
                Instantiate(bulletPrefab, centerPoint.position, Quaternion.identity);
                Instantiate(bulletPrefab, leftPoint.position, Quaternion.identity);
                Instantiate(bulletPrefab, rightPoint.position, Quaternion.identity);
                break;
        }
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.CompareTag("Enemy") || other.CompareTag("EnemyBullet"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
            Debug.Log("Player hit an enemy!");
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        uiManager.Instance.UpdateHealth(currentHealth, maxHealth);
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (audioManager.Instance != null)
        {
            audioManager.Instance.PlaySFX(audioManager.Instance.playerDeathSFX);
        }
        Debug.Log("GAME OVER");
        uiManager.Instance.ShowGameOver();
        Destroy(gameObject);
    }

    void LateUpdate()
    {
        Vector3 currentPos = transform.position;
        float clampedX = Mathf.Clamp(currentPos.x, -10f, 10f);
        float clampedZ = Mathf.Clamp(currentPos.z, -3f, 5f);
        transform.position = new Vector3(clampedX, currentPos.y, clampedZ);
    }
}

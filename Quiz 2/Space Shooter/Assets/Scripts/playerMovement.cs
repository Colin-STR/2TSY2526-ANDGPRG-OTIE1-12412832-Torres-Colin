using UnityEngine;

public class playerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 10f;

    private Rigidbody2D rb;
    private Vector2 moveInput;

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
        rb = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth;
    }

    void Update()
    {
        moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
    }

   public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log("Player Health: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("GAME OVER");
        gameManager.Instance.EndGame();
        Destroy(gameObject);
    }
}

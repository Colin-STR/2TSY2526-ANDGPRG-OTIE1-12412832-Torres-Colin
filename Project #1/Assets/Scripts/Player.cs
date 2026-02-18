using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public float fireRate = 1.5f;
    private float nextFireTime = 0f;
    public int health = 3;

    public float damageCooldown = 1.0f;
    private float nextDamageTime;

    public int maxHealth = 3;
    private UIManager ui;
    void Start()
    {
        Time.timeScale = 1f;
        rb = GetComponent<Rigidbody>();
        ui = FindObjectOfType<UIManager>();
        ui.UpdateHealth(health, maxHealth);
    }

    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 moveInput = new Vector3(horizontal, 0f, vertical).normalized;
        Vector3 moveVelocity = moveInput * moveSpeed;
        rb.linearVelocity = new Vector3(moveVelocity.x, rb.linearVelocity.y, moveVelocity.z);

        if (Input.GetMouseButton(0) && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayDistance;
        if (groundPlane.Raycast(ray, out rayDistance))
        {
            Vector3 pointToLook = ray.GetPoint(rayDistance);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }

        float xBound = 17f;
        float zBound = 5f;
        float clampedX = Mathf.Clamp(transform.position.x, -xBound, xBound);
        float clampedZ = Mathf.Clamp(transform.position.z, -zBound, zBound);
        transform.position = new Vector3(clampedX, transform.position.y, clampedZ);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(transform.position, enemy.transform.position);
            if (dist < 1.5f && Time.time >= nextDamageTime)
            {
               TakeDamage(1);
                nextDamageTime = Time.time + damageCooldown;
                Destroy(enemy);
                FindObjectOfType<WaveManager>().RegisterKill();
            }
        }


    }
    void Shoot()
    {
        Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.shootSound);
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        AudioManager.Instance.PlaySFX(AudioManager.Instance.playerHitSound);
        Debug.Log("Player Health: " + health);
        ui.UpdateHealth(health, maxHealth);
        if (health <= 0)
        {
            Debug.Log("GAME OVER");
            ui.ShowGameOver();
        }
    }
   
}

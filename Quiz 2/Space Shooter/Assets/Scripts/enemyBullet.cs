using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public float speed = 8f;
    public float lifetime = 4f;

    void Start() => Destroy(gameObject, lifetime);

    void Update()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<playerMovement>().TakeDamage(1);
            Destroy(gameObject);
        }
    }
}

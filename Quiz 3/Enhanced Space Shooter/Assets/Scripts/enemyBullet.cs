using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public float speed = 8f;
    public float lifetime = 4f;

    void Start() => Destroy(gameObject, lifetime);

    void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<Player>().TakeDamage(1);
            // gameManager.Instance.AddScore(100);
            Destroy(gameObject);
        }
    }
}

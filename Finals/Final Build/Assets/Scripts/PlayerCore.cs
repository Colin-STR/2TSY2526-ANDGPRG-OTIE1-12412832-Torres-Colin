using UnityEngine;

public class PlayerCore : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            GameManager.instance.TakeDamage(1);

            Destroy(other.gameObject);

            Debug.Log("Core Health updated in GameManager: " + GameManager.instance.coreHealth);
        }
    }
}
using UnityEngine;

public class MonsterHealth : MonoBehaviour
{
    public float health = 100f;
    public int goldReward = 10;

    public void TakeDamage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (audioManager.Instance != null)
            audioManager.Instance.PlaySFX(audioManager.Instance.monsterDeathSFX);
        GameManager.instance.AddGold(goldReward);
        Destroy(gameObject);
    }
}

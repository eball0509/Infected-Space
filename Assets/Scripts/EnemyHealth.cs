using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3;

    [HideInInspector]
    public EnemySpawner spawner;

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (spawner != null)
        {
            spawner.EnemyDestroyed();
        }
        Destroy(gameObject);
    }
}
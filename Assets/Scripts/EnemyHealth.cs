using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 3;

    [HideInInspector]
    public EnemySpawner spawner;

    private EnemyAI ai;

    void Start()
    {
        ai = GetComponent<EnemyAI>();
    }

    public void TakeDamage(int amount)
    {
        if (health <= 0) return;
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (spawner != null)
        {
            spawner.EnemyDestroyed();
        }
        ai?.Die();
        Destroy(gameObject, 2f);
    }
}
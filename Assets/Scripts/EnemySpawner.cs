using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int maxEnemies = 6;
    public float spawnRadius = 2.5f;
    public float minSpawnDistance = 1.5f;
    public float spawnInterval = 3f;

    private int currentEnemies = 0;

    void Start()
    {
        if (enemyPrefab == null)
        {
            enabled = false;
            return;
        }

        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        Transform player = PlayerLocator.GetPlayer();
        if (player == null) return;
        if (currentEnemies >= maxEnemies) return;

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float distance = Random.Range(minSpawnDistance, spawnRadius);

        Vector3 spawnPos = new Vector3(
            player.position.x + Mathf.Sin(angle) * distance,
            XRBootstrap.FloorY + 1f,
            player.position.z + Mathf.Cos(angle) * distance
        );

        GameObject obj = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        EnemyHealth eh = obj.GetComponent<EnemyHealth>();
        if (eh != null) eh.spawner = this;

        currentEnemies++;
    }

    public void EnemyDestroyed()
    {
        currentEnemies--;
    }
}
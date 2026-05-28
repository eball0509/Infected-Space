using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRadius = 2.5f;
    public float minSpawnDistance = 1.5f;
    public float spawnInterval = 3f;
    public float spawnHeightOffset = -0.85f;

    private int enemiesToSpawn = 0;
    private int enemiesSpawned = 0;
    private bool waveActive = false;

    public void StartWave(int enemyCount)
    {
        enemiesToSpawn = enemyCount;
        enemiesSpawned = 0;
        waveActive = true;
        InvokeRepeating(nameof(SpawnEnemy), 1f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (!waveActive) return;

        if (enemiesSpawned >= enemiesToSpawn)
        {
            CancelInvoke(nameof(SpawnEnemy));
            return;
        }

        Transform player = PlayerLocator.GetPlayer();
        if (player == null) return;

        float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
        float distance = Random.Range(minSpawnDistance, spawnRadius);

        float spawnX = player.position.x + Mathf.Sin(angle) * distance;
        float spawnZ = player.position.z + Mathf.Cos(angle) * distance;
        float spawnY = player.position.y + spawnHeightOffset;

        Vector3 spawnPos = new Vector3(spawnX, spawnY, spawnZ);

        GameObject obj = Instantiate(enemyPrefab, spawnPos, Quaternion.identity);
        EnemyHealth eh = obj.GetComponent<EnemyHealth>();
        if (eh != null) eh.spawner = this;

        enemiesSpawned++;
    }

    public void EnemyDestroyed()
    {
        WaveManager.Instance?.OnEnemyKilled();
    }
}
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public GameObject targetPrefab;
    public int targetCount = 10;
    public float spawnRadius = 3f;
    public float minHeight = 0.5f;
    public float maxHeight = 1.8f;

    void Start()
    {
        SpawnTargets();
    }

    void SpawnTargets()
    {
        for (int i = 0; i < targetCount; i++)
        {
            float angle = Random.Range(0f, 360f) * Mathf.Deg2Rad;
            float distance = Random.Range(1.5f, spawnRadius);
            float height = Random.Range(minHeight, maxHeight);

            Vector3 pos = new Vector3(
                Mathf.Sin(angle) * distance,
                height,
                Mathf.Cos(angle) * distance
            );

            Instantiate(targetPrefab, pos, Quaternion.identity);
        }
    }
}
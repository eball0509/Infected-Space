using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 1.2f;
    public float stopDistance = 1.5f;
    public float separationRadius = 0.8f;
    public float separationForce = 1.5f;

    private Transform player;
    private float floatOffset;
    private float spawnY;

    void Start()
    {
        player = PlayerLocator.GetPlayer();
        floatOffset = Random.Range(0f, Mathf.PI * 2f);
        spawnY = transform.position.y;
    }

    void Update()
    {
        if (player == null)
        {
            player = PlayerLocator.GetPlayer();
            return;
        }

        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        float dist = direction.magnitude;

        Vector3 lookTarget = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(lookTarget);

        if (dist > stopDistance)
        {
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
        }

        Collider[] nearby = Physics.OverlapSphere(transform.position, separationRadius);
        foreach (Collider col in nearby)
        {
            if (col.gameObject == gameObject) continue;
            if (!col.GetComponent<EnemyAI>()) continue;

            Vector3 pushDir = transform.position - col.transform.position;
            pushDir.y = 0;
            if (pushDir == Vector3.zero) pushDir = Random.insideUnitSphere;

            float overlap = separationRadius - pushDir.magnitude;
            if (overlap > 0)
            {
                transform.position += pushDir.normalized * overlap * Time.deltaTime * separationForce;
            }
        }

        Vector3 pos = transform.position;
        pos.y = spawnY + Mathf.Sin(Time.time * 3f + floatOffset) * 0.2f;
        transform.position = pos;
    }
}
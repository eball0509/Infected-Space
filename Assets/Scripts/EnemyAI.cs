using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float moveSpeed = 1.2f;
    public float stopDistance = 1.5f;
    public float attackDistance = 1.8f;
    public float separationRadius = 0.8f;
    public float separationForce = 1.5f;
    public float attackCooldown = 2f;
    public float maxWanderDistance = 5f;
    public int damageAmount = 25;

    private Transform player;
    private Animator animator;
    private float attackTimer = 0f;
    private bool isDead = false;
    private bool hasDealtDamageThisAttack = false;

    void Start()
    {
        player = PlayerLocator.GetPlayer();
        animator = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (isDead) return;

        if (player == null)
        {
            player = PlayerLocator.GetPlayer();
            animator.SetFloat("Speed", 0f);
            return;
        }

        Vector3 direction = player.position - transform.position;
        direction.y = 0;
        float dist = direction.magnitude;

        Vector3 lookTarget = new Vector3(player.position.x, transform.position.y, player.position.z);
        transform.LookAt(lookTarget);

        // Attack logic
        attackTimer -= Time.deltaTime;
        if (dist <= attackDistance && attackTimer <= 0f)
        {
            animator.SetBool("Attack", true);
            if (!hasDealtDamageThisAttack)
            {
                PlayerHealth.Instance?.TakeDamage(damageAmount);
                hasDealtDamageThisAttack = true;
            }
            attackTimer = attackCooldown;
        }
        else
        {
            animator.SetBool("Attack", false);
            hasDealtDamageThisAttack = false;
        }

        // Movement
        if (dist > stopDistance)
        {
            transform.position += direction.normalized * moveSpeed * Time.deltaTime;
            animator.SetFloat("Speed", 1f);
        }
        else
        {
            animator.SetFloat("Speed", 0f);
        }

        // Separation
        float distFromPlayer = Vector3.Distance(
            new Vector3(transform.position.x, 0, transform.position.z),
            new Vector3(player.position.x, 0, player.position.z)
        );

        if (distFromPlayer < maxWanderDistance)
        {
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
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        animator.SetBool("Dead", true);
    }
}
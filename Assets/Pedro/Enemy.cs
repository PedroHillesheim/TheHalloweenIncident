using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    private int currentPointIndex = 0;

    [Header("Perseguição")]
    public float chaseRange = 5f;
    public float chaseSpeed = 4f;
    public float attackRange = 1f;

    [Header("Referências")]
    public Transform player;
    public LayerMask wallLayer;

    private Rigidbody2D rb;
    private bool isChasing = false;
    private Vector2 moveDirection;
    private bool playerDead = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    void Update()
    {
        if (playerDead || player == null) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange && HasLineOfSight())
            isChasing = true;
        else if (distanceToPlayer > chaseRange * 1.2f)
            isChasing = false;

        if (isChasing)
            moveDirection = (player.position - transform.position).normalized;
        else
            moveDirection = GetPatrolDirection();

        if (distanceToPlayer <= attackRange && HasLineOfSight())
            AttackPlayer();
    }

    void FixedUpdate()
    {
        if (playerDead) return;
        rb.MovePosition(rb.position + moveDirection * GetCurrentSpeed() * Time.fixedDeltaTime);
    }

    float GetCurrentSpeed()
    {
        return isChasing ? chaseSpeed : patrolSpeed;
    }

    Vector2 GetPatrolDirection()
    {
        if (patrolPoints.Length == 0) return Vector2.zero;

        Transform targetPoint = patrolPoints[currentPointIndex];
        Vector2 dir = (targetPoint.position - transform.position).normalized;

        float dist = Vector2.Distance(transform.position, targetPoint.position);
        if (dist < 0.2f)
        {
            currentPointIndex++;
            if (currentPointIndex >= patrolPoints.Length)
                currentPointIndex = 0;
        }

        return dir;
    }

    bool HasLineOfSight()
    {
        if (player == null) return false;

        RaycastHit2D hit = Physics2D.Linecast(transform.position, player.position, wallLayer);
        return hit.collider == null;
    }

    void AttackPlayer()
    {
        if (playerDead) return;
        playerDead = true;

        Debug.Log("Inimigo atacou o jogador! GAME OVER!");

        // Notifica o gerenciador de Game Over
        GameOverManager.Instance.ShowGameOver();
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

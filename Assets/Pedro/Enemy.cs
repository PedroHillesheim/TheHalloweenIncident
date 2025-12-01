using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public Transform[] patrolPoints;
    public float patrolSpeed = 2f;
    public float waitTimeAtPoint = 2f;
    private int currentPointIndex = 0;
    private bool movingForward = true;
    private bool isWaiting = false;
    public float chaseRange = 5f;
    public float chaseSpeed = 4f;
    public float attackRange = 1f;
    public Transform player;
    public LayerMask wallLayer;
    private Rigidbody2D rb;
    private bool isChasing = false;
    private Vector2 moveDirection;
    private bool playerDead = false;
    public GameObject loseScreen;

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
        if (playerDead || player == null || isWaiting) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseRange && HasLineOfSight())
            isChasing = true;
        else if (distanceToPlayer > chaseRange * 1.3f)
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
        if (playerDead || isWaiting) return;
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
        if (dist < 0.2f && !isWaiting)
        {
            StartCoroutine(WaitAtPoint());
        }
        return dir;
    }
    IEnumerator WaitAtPoint()
    {
        isWaiting = true;
        rb.linearVelocity = Vector2.zero;
        yield return new WaitForSeconds(waitTimeAtPoint);
        if (movingForward)
        {
            currentPointIndex++;
            if (currentPointIndex >= patrolPoints.Length)
            {
                currentPointIndex = patrolPoints.Length - 2;
                movingForward = false;
            }
        }
        else
        {
            currentPointIndex--;
            if (currentPointIndex < 0)
            {
                currentPointIndex = 1;
                movingForward = true;
            }
        }
        isWaiting = false;
    }

    bool HasLineOfSight()
    {
        if (player == null)
        {
            return false;
        }

            if (wallLayer == 0)
        {
            return true;
        }
        Vector2 start = transform.position;
        Vector2 end = player.position;

        RaycastHit2D[] hits = Physics2D.LinecastAll(start, end);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider != null && hit.collider.gameObject != gameObject)
            {
                if (((1 << hit.collider.gameObject.layer) & wallLayer) != 0)
                {
                    return false;
                }
                    
            }
        }

        return true;
    }

    void AttackPlayer()
    {
        if (playerDead) return;
        playerDead = true;
        loseScreen.SetActive(true);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}

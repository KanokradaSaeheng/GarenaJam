using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float chaseRange = 10f;


    private Vector3 startPosition;
    private bool isChasing = false;

    private void Start()
    {
        startPosition = transform.position; // Save starting spot
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float distanceToHome = Vector3.Distance(transform.position, startPosition);

        // === Player in range? Chase ===
        if (distanceToPlayer <= chaseRange)
        {
            isChasing = true;
            ChasePlayer(distanceToPlayer);
        }
        // === Player out of range? Return ===
        else if (isChasing)
        {
            ReturnToStart(distanceToHome);
        }
    }

    private void ChasePlayer(float distance)
    {
       

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Face the player
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    private void ReturnToStart(float distance)
    {
        if (distance <= 0.1f)
        {
            isChasing = false; // Fully returned
            return;
        }

        Vector3 direction = (startPosition - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        // Face home position
        transform.LookAt(new Vector3(startPosition.x, transform.position.y, startPosition.z));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}

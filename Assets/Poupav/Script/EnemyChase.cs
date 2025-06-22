using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    public float chaseRange = 10f;

    public GameObject sparkEffectPrefab; // Drag your spark effect prefab here

    private Vector3 startPosition;
    private bool isChasing = false;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (player == null) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float distanceToHome = Vector3.Distance(transform.position, startPosition);

        if (distanceToPlayer <= chaseRange)
        {
            isChasing = true;
            ChasePlayer(distanceToPlayer);
        }
        else if (isChasing)
        {
            ReturnToStart(distanceToHome);
        }
    }

    private void ChasePlayer(float distance)
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    private void ReturnToStart(float distance)
    {
        if (distance <= 0.1f)
        {
            isChasing = false;
            return;
        }

        Vector3 direction = (startPosition - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        transform.LookAt(new Vector3(startPosition.x, transform.position.y, startPosition.z));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // 💥 Spawn Spark Effect at contact point
            if (sparkEffectPrefab != null)
            {
                ContactPoint contact = collision.contacts[0];
                Instantiate(sparkEffectPrefab, contact.point, Quaternion.identity);
            }

            Destroy(gameObject); // Then destroy the enemy
        }
    }
}

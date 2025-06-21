using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyUnit : MonoBehaviour
{
    public Transform playerTarget;
    public float moveSpeed = 5f;
    public float detectionRadius = 5f;
    public float health = 3f;
  


    private Transform currentEnemyTarget;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Search for enemies if not already targeting one
        if (currentEnemyTarget == null)
        {
            Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    currentEnemyTarget = hit.transform;
                    break;
                }
            }
        }

        // Move to enemy or follow player
        if (currentEnemyTarget != null)
        {
            MoveTo(currentEnemyTarget.position);
        }
        else if (playerTarget != null)
        {
            MoveTo(playerTarget.position);
        }
    }

    void MoveTo(Vector3 targetPos)
    {
        Vector3 direction = (targetPos - transform.position).normalized;
        rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
        transform.forward = direction; // face the direction
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1f, this.gameObject); // 💥 pass self to allow conversion
            }

            health -= 1f;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
    
}
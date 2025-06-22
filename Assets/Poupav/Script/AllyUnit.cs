using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyUnit : MonoBehaviour
{
    public Transform playerTarget;
    public float moveSpeed = 5f;
    public float detectionRadius = 5f;
    public float health = 3f;

    public GameObject sparkEffectPrefab; // 🔥 Drag your spark prefab here

    private Transform currentEnemyTarget;
    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector3 targetPos = Vector3.zero;
        bool hasTarget = false;

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

        if (currentEnemyTarget != null)
        {
            targetPos = currentEnemyTarget.position;
            hasTarget = true;
        }
        else if (playerTarget != null)
        {
            targetPos = playerTarget.position;
            hasTarget = true;
        }

        if (hasTarget)
        {
            MoveTo(targetPos);
        }
        else
        {
            animator.SetFloat("Speed", 0f); // idle
        }
    }

    void MoveTo(Vector3 targetPos)
    {
        Vector3 direction = (targetPos - transform.position);
        float distance = direction.magnitude;

        if (distance > 0.05f)
        {
            Vector3 moveDir = direction.normalized;
            rb.MovePosition(transform.position + moveDir * moveSpeed * Time.deltaTime);
            transform.forward = moveDir;

            animator.SetFloat("Speed", moveSpeed); // walking
        }
        else
        {
            animator.SetFloat("Speed", 0f); // idle
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // 💥 Spawn spark effect at contact point
            if (sparkEffectPrefab != null && collision.contacts.Length > 0)
            {
                ContactPoint contact = collision.contacts[0];
                Instantiate(sparkEffectPrefab, contact.point, Quaternion.identity);
            }

            Enemy enemy = collision.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(1f, this.gameObject);
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

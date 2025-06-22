using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public float attackRange = 2f;
    public float damage = 20f;
    public float attackCooldown = 2f;

    private float lastAttackTime = 0f;
    public Transform attackTarget; // Assign Player Transform

    void Update()
    {
        if (attackTarget == null) return;

        float distance = Vector3.Distance(transform.position, attackTarget.position);

        if (distance <= attackRange && Time.time >= lastAttackTime + attackCooldown)
        {
            lastAttackTime = Time.time;
            Attack();
        }
    }

    void Attack()
    {
        Debug.Log("Enemy attacks!");

        PlayerHealth player = attackTarget.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}

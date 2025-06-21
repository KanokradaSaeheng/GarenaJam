using UnityEngine;

public class enemy : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float damage = 25f;
    public GameObject destroyEffect;

    private GameObject targetTower;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        FindClosestTower();
    }

    void FixedUpdate()
    {
        if (targetTower == null)
        {
            FindClosestTower();
            return;
        }

        Vector3 direction = (targetTower.transform.position - transform.position).normalized;
        direction.y = 0; // ป้องกันไม่ให้ลอยขึ้น/ลง

        Vector3 move = direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tower2"))
        {
            // เปลี่ยนจาก Tower → Tower2
            Tower2 tower = collision.gameObject.GetComponent<Tower2>();
            if (tower != null)
            {
                tower.TakeDamage(damage);
            }

            if (destroyEffect != null)
            {
                Instantiate(destroyEffect, transform.position, Quaternion.identity);
            }

            Destroy(gameObject);
        }
    }

    void FindClosestTower()
    {
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower2");

        float closestDistance = Mathf.Infinity;
        GameObject closestTower = null;

        foreach (GameObject tower in towers)
        {
            float distance = Vector3.Distance(transform.position, tower.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestTower = tower;
            }
        }

        targetTower = closestTower;
    }
}

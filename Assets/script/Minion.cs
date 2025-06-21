using UnityEngine;

public class Minion : MonoBehaviour
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

        // รักษาระดับ Y (พื้น) ให้ไม่ลอยขึ้น/ลง
        direction.y = 0;

        Vector3 move = direction * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Tower"))
        {
            Tower tower = collision.gameObject.GetComponent<Tower>();
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
        GameObject[] towers = GameObject.FindGameObjectsWithTag("Tower");

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

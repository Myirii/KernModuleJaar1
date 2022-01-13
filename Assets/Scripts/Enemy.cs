using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType { none, weak, normal, strong }
    public EnemyType thisEnemy;

    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private Transform firePoint;

    private float shootTimer = 0.0f;
    private float maxShootTime = 1f;

    private void FixedUpdate()
    {
        shootTimer -= Time.fixedDeltaTime;

        Debug.DrawLine(transform.position, transform.position - new Vector3(10, 0, 0), Color.black);

        RaycastHit2D hit = Physics2D.Linecast(transform.position, transform.position - new Vector3(10, 0, 0));

        if (shootTimer <= 0 && hit.collider != null && hit.collider.CompareTag("Player"))
        {
            Instantiate(prefabBullet, firePoint.position, Quaternion.identity);
            shootTimer = maxShootTime;
        }
    }
}

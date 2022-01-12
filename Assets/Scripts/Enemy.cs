using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType { none, weak, normal, strong }
    public EnemyType thisEnemy;

    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private Transform firePoint;

    private float shootTimer = 0.0f;
    private float maxShootTime = 0.5f;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        shootTimer -= Time.fixedDeltaTime;

        Debug.DrawLine(transform.position, transform.position - new Vector3(10, 0, 0), Color.black);
        if (Physics2D.Linecast(transform.position, transform.position - new Vector3(10, 0, 0)))
        {
            Instantiate(prefabBullet, firePoint.position, Quaternion.identity);
            shootTimer = maxShootTime;
        }
    }
}

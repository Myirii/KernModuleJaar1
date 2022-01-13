using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int bulletStrength;
    [SerializeField] private float bulletSpeed;

    private void FixedUpdate()
    {
        transform.position -= new Vector3(bulletSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D _coll)
    {
        if (_coll.gameObject.tag == "Player")
        {
            if (bulletStrength > _coll.gameObject.GetComponent<PlayerMovement>().shieldStrength)
            {
                //jurpaca trample
            }
            else
            {
                _coll.gameObject.GetComponent<PlayerMovement>().CreateShieldHitstun(bulletStrength);
            }

            Destroy(gameObject);
        }
    }
}

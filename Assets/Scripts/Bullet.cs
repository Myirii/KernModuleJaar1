using UnityEngine;
using UnityEngine.SceneManagement;

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
                SceneManager.LoadScene(0);
                ScoreTextScript.coinAmount = 0;
            }
            else
            {
                _coll.gameObject.GetComponent<PlayerMovement>().CreateShieldHitstun(bulletStrength);
            }

            Destroy(gameObject);
        }
    }
}

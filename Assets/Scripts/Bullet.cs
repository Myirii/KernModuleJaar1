using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int bulletStrength;

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
        }
    }
}

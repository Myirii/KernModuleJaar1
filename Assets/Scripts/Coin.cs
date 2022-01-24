using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value;

    private ParticleSystem ps;

    private void Awake()
    {
		ps = transform.GetChild(0).GetComponent<ParticleSystem>();
	}

	private void OnTriggerEnter2D(Collider2D _coll)
	{
		if (_coll.gameObject.tag == "Player")
		{
			ScoreTextScript.coinAmount += value;
			//play collection animation
			ps.Play();
			//turn off collider and add coin
			GetComponent<BoxCollider2D>().enabled = false;
			//turn off spriterenderer so the coin seems gone
			GetComponent<SpriteRenderer>().enabled = false;
		}
	}
}

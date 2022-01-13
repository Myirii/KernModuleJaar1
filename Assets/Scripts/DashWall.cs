using UnityEngine;

public class DashWall : MonoBehaviour
{
    public enum WallType { none, weak, normal, strong }
    public WallType thisWall;

    private PlayerMovement pm;

    [SerializeField] private int breakStrength;

    private void Awake()
    {
        pm = GameObject.Find("Player").GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        switch (thisWall)
        {
            case WallType.weak:
                breakStrength = pm.dashing ? (int)(pm.baseSpeed - pm.dashBonus) + 3 : (int)pm.baseSpeed + 3;
                break;
            case WallType.normal:
                breakStrength = pm.dashing ? (int)(pm.baseSpeed - pm.dashBonus) + 6 : (int)pm.baseSpeed + 6;
                break;
            case WallType.strong:
                breakStrength = pm.dashing ? (int)(pm.baseSpeed - pm.dashBonus) + 9 : (int)pm.baseSpeed + 9;
                break;
            case WallType.none:
            default:
                break;
        }
    }

    private void OnCollisionStay2D(Collision2D _coll)
    {
        if (_coll.gameObject.tag == "Player")
        {
            if (pm.dashing && pm.baseSpeed > breakStrength)
            {
                GetComponent<BoxCollider2D>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
            }
        }
    }
}

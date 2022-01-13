using UnityEngine;

public class AlpacaHorde : MonoBehaviour
{
    [SerializeField] private float playerXoffset;
    [SerializeField] private Transform player;

    private void LateUpdate()
    {
        transform.position = new Vector3(player.position.x - playerXoffset, transform.position.y);
    }
}

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector2 yBounds;
    [SerializeField] private Transform lookPoint;

    private void LateUpdate()
    {
        if (lookPoint != null)
        {
            transform.position = new Vector3(lookPoint.position.x, Mathf.Min(Mathf.Max(lookPoint.position.y, yBounds.x), yBounds.y), -10);
        }
    }
}

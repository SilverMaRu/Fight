using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawBorder : MonoBehaviour
{
    public enum Direction
    {
        Vertical,
        Horizontal
    }
    public Direction direction = Direction.Vertical;
    public Color color = Color.yellow;
    public float length = 5;

    private void OnDrawGizmos()
    {
        Vector3 position = transform.position;
        Vector3 form = Vector3.zero;
        Vector3 to = Vector3.zero;
        float halfLength = length * .5f;
        switch (direction)
        {
            case Direction.Vertical:
                form = Vector3.right * position.x + Vector3.up * (position.y - halfLength) + Vector3.forward * position.z;
                to = form + Vector3.up * length;
                break;
            case Direction.Horizontal:
                form = Vector3.right * (position.x - halfLength) + Vector3.up * position.y + Vector3.forward * position.z;
                to = form + Vector3.right * length;
                break;
        }
        Gizmos.color = color;
        Gizmos.DrawLine(form, to);
    }
}

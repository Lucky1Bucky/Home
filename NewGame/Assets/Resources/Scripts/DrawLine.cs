using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawLine : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        for (int i = 1; i < transform.childCount; i++)
        {
            Gizmos.DrawLine(transform.GetChild(i-1).position, transform.GetChild(i).position);
        }
    }
}

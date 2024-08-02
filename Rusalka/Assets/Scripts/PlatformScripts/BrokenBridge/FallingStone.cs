using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FallingStone : MonoBehaviour, IBridgeCallable
{
    public Vector2 endPosition;
    
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + endPosition);
        
        Gizmos.DrawSphere((Vector2)transform.position + endPosition, 0.1f);

        Gizmos.color = Color.red;
    }

    public void FireBreak()
    {
        transform.position = (Vector2)transform.position + endPosition;
    }
}

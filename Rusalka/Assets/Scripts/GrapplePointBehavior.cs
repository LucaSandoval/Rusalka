using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePointBehavior : MonoBehaviour
{
    // Distance that grapple points can be targetted
    public float TriggerRange = 5f;

    private bool interactible = true;

    public bool DrawDebug = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Draw the debug circle in the game view
        if (DrawDebug)
        {
            DrawCircle(transform.position, TriggerRange, Color.red);
        }
    }

    private void DrawCircle(Vector3 center, float radius, Color color)
    {
        int segments = 36; // Number of segments to approximate the circle
        float angle = 0f;
        Vector3 prevPoint = center + new Vector3(radius, 0f, 0f);
        Color previousColor = Gizmos.color; // Save previous Gizmos color

        for (int i = 0; i <= segments; i++)
        {
            angle += Mathf.PI * 2 / segments;
            Vector3 newPoint = center + new Vector3(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius, 0f);
            Debug.DrawLine(prevPoint, newPoint, color);
            prevPoint = newPoint;
        }
    }

    // Method to temporarily disable the interactibility of the grapple point
    public void DisableInteractibility(float duration)
    {
        if (interactible)
        {
            interactible = false;
            StartCoroutine(ReenableInteractibility(duration));
        }
    }

    // Coroutine to re-enable interactibility after a delay
    private IEnumerator ReenableInteractibility(float duration)
    {
        yield return new WaitForSeconds(duration);
        interactible = true;
    }

    // Method to check if the grapple point is interactible
    public bool IsInteractible()
    {
        return interactible;
    }
}

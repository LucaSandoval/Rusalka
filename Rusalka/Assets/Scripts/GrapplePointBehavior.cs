using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Class that Defines the behavior for the grapple points
 */
public class GrapplePointBehavior : MonoBehaviour
{
    // Distance that grapple points can be targetted
    [Tooltip("Interactible radius around grapple point")]
    public float TriggerRange = 5f;
    [SerializeField] private bool DrawDebug = false;
    private LineRenderer lineRenderer;
    private int Segments = 36;
    public bool renderRadius;
    [SerializeField] private GameObject reticle;

    [Tooltip("Speed the player will fly to the grapple point with")]
    public float GrappleEnterSpeed = 25f;
    [Tooltip("Speed that the player will be launched once they reach the grapple point")]
    public float GrappleExitSpeed = 25f;

    // Is this grapple point a selectible target
    private bool interactible = true;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = Segments + 1;
        lineRenderer.useWorldSpace = true;

        if (renderRadius)
        {
            DrawVisibleCircle();
        }
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

    // Draw Debug Circle in the editor during runtime
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
            reticle.SetActive(false);
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

   /*
    * Enables line renderer that draws radius
    */
    private void DrawVisibleCircle()
    {
        float angle = 0f;
        for (int i = 0; i <= Segments; i++)
        {
            angle += Mathf.PI * 2 / Segments;
            Vector3 newPoint = transform.position + new Vector3(Mathf.Cos(angle) * TriggerRange, Mathf.Sin(angle) * TriggerRange, 0f);
            lineRenderer.SetPosition(i, newPoint);
        }
    }

    /*
     * Toggles reticle on grapple point 
     */
    public void ToggleReticle(bool toggle)
    {
        reticle.SetActive(toggle);
    }
}

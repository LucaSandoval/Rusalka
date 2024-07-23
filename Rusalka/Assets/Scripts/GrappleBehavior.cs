using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


/*
 * Class that defines the behavior of the grapple mechanic
 *
 *@author Kevin Rodriguez
 */
public class GrappleBehavior : MonoBehaviour
{
    // Reference to player
    public GameObject Player;

    [Tooltip("Speed the player will fly to the grapple point with")]
    public float GrappleSpeed = 500f;
    
    [Tooltip("The time that the player will be unable to grapple to the same grapple point again")]
    public float GrapplePointExhaustionTime = 0.5f;
    public bool DrawDebug = false;
    public float GrappleHairRenderPositionOffset;

    private (bool, Vector2) BestGrapplePoint;
    private GrapplePointBehavior BestPoint = null;
    private Vector2 BestGrapplePosition = Vector2.zero;
    private GameObject[] GrapplePoints;
    private PlayerController PlayerController;
    private LineRenderer LineRenderer;
    private bool InGrapple;


    // Start is called before the first frame update
    void Start()
    {
        LineRenderer = Player.GetComponent<LineRenderer>();
        SetupLineRender();
        GrapplePoints = GameObject.FindGameObjectsWithTag("GrapplePoint");
        BestGrapplePoint = (false, Vector2.zero);
        PlayerController = Player.GetComponent<PlayerController>();
        GrappleHairRenderPositionOffset = 0.5f;
        
    }

    // Update is called once per frame
    void Update()
    {
        TargetGrapplePoint();
          if (Input.GetAxisRaw("Fire1") > 0 || Input.GetKeyDown(KeyCode.X)) {
            GrappleToPoint();
          }
        UpdateLine();
        
        // Draw the right vector (direction character is facing)
        //Debug.DrawLine(transform.position, transform.position + transform.right * 2, Color.red);

        // Draw the left vector (opposite direction)
        //Debug.DrawLine(transform.position, transform.position - transform.right * 2, Color.blue);
    }

    // Analyzes all grapple points available and returns if there is a optimal point, and its direction
    private void TargetGrapplePoint()
    {
        float bestDistance = float.MaxValue;
        bool pointAvailable = false;
        Vector2 directionToBestPoint = Vector2.zero;
        Vector2 bestGrapplePoint = Vector2.zero;
        
        foreach(GameObject point in GrapplePoints) { 
            // Reference to script with grapple point behavior 
            GrapplePointBehavior pointBehavior = point.GetComponent<GrapplePointBehavior>();

            float distanceToPoint = Vector2.Distance(transform.position, point.transform.position);
            // If our distance to point is within range, then continue operating
            if (distanceToPoint <= pointBehavior.TriggerRange) 
            {
                Vector2 directionToPoint = point.transform.position - gameObject.transform.position;

                //Determine if the selected point is facing in your direction
                bool forwardFacing = Vector2.Dot(Player.GetComponent<PlayerController>().Facing(), directionToPoint.normalized) >= 0;
                if (forwardFacing && directionToPoint.y > -1)
                {
                    if (pointBehavior.IsInteractible())
                    {
                        // If current distance is the shortest we have seen, then make it the most optimal point
                        if (distanceToPoint < bestDistance)
                        {
                            bestDistance = distanceToPoint;
                            directionToBestPoint = directionToPoint;
                            pointAvailable = true;
                            bestGrapplePoint = point.transform.position;
                            BestPoint = pointBehavior;
                            BestGrapplePosition = point.transform.position;
                        }
                    }  
                }
            }
            
        }
        if (pointAvailable && DrawDebug)
        {
            Debug.DrawLine(transform.position, bestGrapplePoint, Color.green);
        }
        BestGrapplePoint = (pointAvailable, directionToBestPoint);
    }

    /*
     * Launches the player with a given speed to the designated best grapple point if available
     */
    private void GrappleToPoint()
    {
        Rigidbody2D rb = Player.GetComponent<Rigidbody2D>();
        if (BestGrapplePoint.Item1)
        {
            BestPoint.DisableInteractibility(GrapplePointExhaustionTime);
            PlayerController.SetVelocity(BestGrapplePoint.Item2.normalized * GrappleSpeed, true);
            InGrapple = true;
        }
    }
    
    // Setup the initial parameters for the line render
    private void SetupLineRender()
    {
        LineRenderer.positionCount = 2;
        LineRenderer.SetPosition(0, transform.position);
        LineRenderer.SetPosition(1, transform.position);
        LineRenderer.enabled = false;
    }

    // Update the line render for a frame
    private void UpdateLine()
    {
        print(InGrapple);
        if (InGrapple)
        {
            LineRenderer.enabled = true;
            Vector2 originPosition = new Vector2(transform.position.x, transform.position.y + GrappleHairRenderPositionOffset);
            LineRenderer.SetPosition(0, originPosition);
            LineRenderer.SetPosition(1, BestGrapplePosition);
        }
        else
        {
            LineRenderer.enabled = false;
        }
        
        
    }

    // Set InGrapple boolean
    public void SetInGrapple(bool inGrapple)
    {
        InGrapple = inGrapple;
    }
}

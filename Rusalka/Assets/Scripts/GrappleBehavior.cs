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
    GameObject[] grapplePoints;
    [Tooltip("Speed the player will fly to the grapple point with")]
    public float GrappleSpeed = 500f;
    private GrapplePointBehavior bestPoint = null;
    [Tooltip("The time that the player will be unable to grapple to the same grapple point again")]
    public float GrapplePointExhaustionTime = 0.5f;
    public bool DrawDebug = false;

    private (bool, Vector2) BestGrapplePoint;

    // Start is called before the first frame update
    void Start()
    {
        grapplePoints = GameObject.FindGameObjectsWithTag("GrapplePoint");
        BestGrapplePoint = (false, Vector2.zero);
    }

    // Update is called once per frame
    void Update()
    {
        TargetGrapplePoint();
          if (Input.GetKeyDown(KeyCode.LeftShift)) {
            GrappleToPoint();
            }
        
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
        
        foreach(GameObject point in grapplePoints) { 

            // Reference to script with grapple point behavior 
            GrapplePointBehavior pointBehavior = point.GetComponent<GrapplePointBehavior>();

            float distanceToPoint = Vector2.Distance(transform.position, point.transform.position);
            // If our distance to point is within range, then continue operating
            if (distanceToPoint <= pointBehavior.TriggerRange) 
            {
                Vector2 directionToPoint = point.transform.position - gameObject.transform.position;

                //Determine if the selected point is facing in your direction
                bool forwardFacing = Vector2.Dot(Player.GetComponent<PlayerController>().Facing(), directionToPoint.normalized) >= 0;
                if (forwardFacing)
                {
                    if (pointBehavior.IsInteractible())
                    {
                        // If current distance is the shortest we have seen, then make it the most optimal point
                        if (distanceToPoint < bestDistance)
                        {
                            print("We found a point!");
                            bestDistance = distanceToPoint;
                            directionToBestPoint = directionToPoint;
                            pointAvailable = true;
                            bestGrapplePoint = point.transform.position;
                            bestPoint = pointBehavior;
                        }
                    }
                    
                }
            }
            else
            {
                print("Womp Womp");
            }
        }
        if (pointAvailable)
        {
            if (DrawDebug)
            {
                Debug.DrawLine(transform.position, bestGrapplePoint, Color.green);
            }
            
        }
        BestGrapplePoint = (pointAvailable, directionToBestPoint);
    }

    private void GrappleToPoint()
    {
        Rigidbody2D rb = Player.GetComponent<Rigidbody2D>();
        if (BestGrapplePoint.Item1)
        {
            bestPoint.DisableInteractibility(GrapplePointExhaustionTime);
            rb.AddForce(BestGrapplePoint.Item2.normalized * GrappleSpeed);
        }
    }
}

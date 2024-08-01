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

    [Tooltip("The time that the player will be unable to grapple to the same grapple point again")]
    [SerializeField] private float GrapplePointExhaustionTime = 0.5f;
    [SerializeField] private bool DrawDebug = false;
    [Tooltip("Offset from the center of the player to shift the y-axis of the line render")]
    [SerializeField] private float GrappleHairRenderPositionOffset;

    private (bool, Vector2) BestGrapplePoint;
    private GrapplePointBehavior BestPoint = null;
    private Vector2 BestGrapplePosition = Vector2.zero;
    private GameObject[] GrapplePoints;
    private PlayerController PlayerController;
    private LineRenderer LineRenderer;
    private bool InGrapple;

    [SerializeField]
    [Range(0f, 1f)]
    [Tooltip("Margin of error for the angle that a grapple point can be behing you and still targetable")]
    private float GrappleAngleForgiveness;
    private float DistanceToGrapple = 0;
    private Vector2 OriginalPosition = Vector2.zero;
    [SerializeField] private bool DevDebugMovement = false;
    private Vector2 lastKnownAngleOfLaunch = Vector2.zero;


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
        if (Input.GetButtonDown("Fire1"))
        { 
            GrappleSpeedBoost();          
        }
        // Tool for developers to move freely in the scene
        if (DevDebugMovement && Input.GetAxisRaw("Fire2") > 0) {
            Vector2 dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            PlayerController.SetVelocity(dashDirection * 30); 
          }
        UpdateLine();
        if (InGrapple)
        {
            if (DistanceToGrapple <= Vector2.Distance(OriginalPosition, transform.position))
            {
                InGrapple = false;
                PlayerController.SetInGrapple(false);
                OutOfGrappleLaunch();
            }
        }
        
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
                RaycastHit2D hit = Physics2D.BoxCast(transform.position, new Vector2(0.5f, 1.7f), 0, directionToPoint.normalized, distanceToPoint, LayerMask.GetMask("Floor", "Slope"));
                if (DrawDebug) Debug.DrawRay(transform.position, directionToPoint.normalized * distanceToPoint, Color.red);
                //Determine if the selected point is facing in your direction
                bool forwardFacing = Vector2.Dot(Player.GetComponent<PlayerController>().Facing(), directionToPoint.normalized) >= -GrappleAngleForgiveness;
                if (forwardFacing && directionToPoint.y > -1 && hit.collider == null)
                {
                    if (pointBehavior.IsInteractible())
                    {
                        pointBehavior.ToggleReticle(true);
                        // If current distance is the shortest we have seen, then make it the most optimal point
                        if (distanceToPoint < bestDistance)
                        {
                            bestDistance = distanceToPoint;
                            directionToBestPoint = directionToPoint;
                            pointAvailable = true;
                            bestGrapplePoint = point.transform.position;
                            BestPoint = pointBehavior;
                            BestGrapplePosition = point.transform.position;
                            DistanceToGrapple = distanceToPoint;
                        }
                    }  
                }
                else
                {
                    pointBehavior.ToggleReticle(false);
                }
            }
            else
            {
                pointBehavior.ToggleReticle(false);
            }
        }
        if (pointAvailable && DrawDebug) Debug.DrawLine(transform.position, bestGrapplePoint, Color.green);
        BestGrapplePoint = (pointAvailable, directionToBestPoint);
    }

    /*
     * Launches the player with a given speed to the designated best grapple point if available
     */
    private void GrappleSpeedBoost()
    { 
        if (BestGrapplePoint.Item1)
        {
            if (InGrapple) BestPoint.DisableInteractibility(GrapplePointExhaustionTime);
            PlayerController.SetVelocity(BestGrapplePoint.Item2.normalized * BestPoint.GrappleEnterSpeed, true);
            PlayerController.SetGrounded(false);
            InGrapple = true;
            OriginalPosition = transform.position;
            lastKnownAngleOfLaunch = BestGrapplePoint.Item2.normalized;
            SoundController.Instance?.PlaySoundOneShotRandomPitch("Grapple", 0.05f);
        }
    }

    /*
     * Setup the initial parameters for the line render
     */
    private void SetupLineRender()
    {
        //Temporary for now until we have more assets for the hair grapple animation
        LineRenderer.positionCount = 2;
        LineRenderer.SetPosition(0, transform.position);
        LineRenderer.SetPosition(1, transform.position);
        LineRenderer.enabled = false;
    }

    /*
     * Update the line render for a frame based on current state
     */
    private void UpdateLine()
    {
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

    /*
     * Responsible for setting speed of player out of grapple point
     */
    private void OutOfGrappleLaunch()
    {
        PlayerController.SetVelocity(lastKnownAngleOfLaunch * BestPoint.GrappleExitSpeed);
    }
}
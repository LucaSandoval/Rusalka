using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * Class that defines the behavior of the grapple mechanic
 *
 *@author Kevin Rodriguez
 */
public class GrappleBehavior : MonoBehaviour
{
    // Reference to player
    public GameObject Player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        TargetGrapplePoint();
    }

    // Analyzes all grapple points available and returns if there is a optimal point, and its location
    private (bool, Vector2) TargetGrapplePoint()
    {
        GameObject[] grapplePoints = GameObject.FindGameObjectsWithTag("GrapplePoint");

        Vector2 bestGrapplePoint = Vector2.zero;
        float bestDistance = float.MaxValue;
        bool pointAvailable = false;

        foreach(GameObject point in grapplePoints) { 

            // Reference to script with grapple point behavior 
            GrapplePointBehavior pointBehavior = point.GetComponent<GrapplePointBehavior>();

            float distanceToPoint = Vector2.Distance(transform.position, point.transform.position);
            // If our distance to point is within range, then continue operating
            if (distanceToPoint <= pointBehavior.TriggerRange) 
            {
                Vector2 directionToPoint = point.transform.position - gameObject.transform.position;

                //Determine if the selected point is facing in your direction
                bool forwardFacing = Vector2.Dot(transform.forward, directionToPoint.normalized) >= 0;
                if (forwardFacing)
                {
                    // If current distance is the shortest we have seen, then make it the most optimal point
                    if (distanceToPoint < bestDistance)
                    {
                        print("We found a point!");
                        bestDistance = distanceToPoint;
                        bestGrapplePoint = point.transform.position;
                        pointAvailable = true;
                    }
                }
            }
            print("Womp Womp");
        }

        return (pointAvailable, bestGrapplePoint);
    }
}

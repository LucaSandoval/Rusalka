using System.Collections.Generic;
using UnityEngine;

public class FlyFish : MonoBehaviour {

    [SerializeField] private List<Transform> points;
    [SerializeField] private Transform fish;

    public delegate void ColliderListener();
    public static ColliderListener OnCollisionFired;

    private float interpolateAmount;
    [SerializeField] private float playingSpeed = 0.4f;

    [SerializeField] private Animator anim;

    private bool hasFired = false;
    
    [Header("Timer")]
    [SerializeField] float targetTime = 2.0f;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        
        OnCollisionFired += Fired;
    }

    private void Fired()
    {
        hasFired = true;
    }

    private void Update() {
        if(!hasFired) return;
        
        interpolateAmount = (interpolateAmount + Time.deltaTime * playingSpeed);
        
        if (interpolateAmount % 1f == 0f)
        {
            enabled = false;
            Destroy(this);
        }
        
        targetTime -= Time.deltaTime;
        if(targetTime <= 0.0f)
        {
            anim.SetTrigger("IsFlying");
        }

        if (points.Count >= 2) {
            Vector3 previousPosition = fish.position;
            fish.position = Interpolate(points, interpolateAmount);
            
            //rotate the fish sprite to the direction of the point.
            Vector3 direction = (fish.position - previousPosition).normalized;
            if (direction != Vector3.zero)
            {
                fish.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            }
        }
    }

    private Vector3 Interpolate(List<Transform> points, float t) {
        //Convert to Vector3 for convenience sake.
        List<Vector3> positions = new List<Vector3>();
        foreach (var point in points) {
            positions.Add(point.position);
        }
        
        while (positions.Count > 1) { //Stop when the result position is found.
            List<Vector3> nextPositions = new List<Vector3>();
            //Interpolate between i and i+1, store result in a list.
            for (int i = 0; i < positions.Count - 1; i++) {
                Vector3 interpolatedPosition = Vector3.Lerp(positions[i], positions[i + 1], t);
                nextPositions.Add(interpolatedPosition);
            }
            //Assigning result positions to current to interpolate between them in the next iteration of while.
            positions = nextPositions;
        }
        return positions[0];
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyFish : MonoBehaviour {

    [SerializeField] private List<Transform> points;
    [SerializeField] private Transform fish;

    private float interpolateAmount;
    [SerializeField] private float playingSpeed = 0.4f;

    private void Update() {
        interpolateAmount = (interpolateAmount + Time.deltaTime * playingSpeed) % 1f;

        if (points.Count >= 2) {
            Vector3 previousPosition = fish.position;
            fish.position = Interpolate(points, interpolateAmount);

            Vector3 direction = (fish.position - previousPosition).normalized;
            if (direction != Vector3.zero)
            {
                Debug.Log(direction);
                fish.rotation = Quaternion.LookRotation(Vector3.forward, direction);
                //fish.rotation = Quaternion.Euler(fish.rotation.x, fish.rotation.y, fish.rotation.z + 45.0f);
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
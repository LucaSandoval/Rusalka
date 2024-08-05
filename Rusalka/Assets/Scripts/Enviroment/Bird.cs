using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 0.1f;
    
    void FixedUpdate()
    {
        Vector3 newPosition = transform.position + Vector3.right * movingSpeed;
        transform.position = newPosition;
    }
}

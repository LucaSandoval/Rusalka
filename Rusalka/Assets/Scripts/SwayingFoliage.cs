using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwayingFoliage : MonoBehaviour
{
    public GameObject SwayingSprite;
    private const float lerpSpeed = 5f;
    private const float swayDeceleration = 100f;

    private Quaternion targetAngle;
    private float targetAngleZ;

    private void Start()
    {
        //AddSwayAngle(-45);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            if (playerController != null)
            {
                float angle = 30 * Mathf.Sign(playerController.GetMovementVelocity().x);
                AddSwayAngle(-angle);
            }
        }
    }
    private void AddSwayAngle(float angle)
    {
        float angleCap = 45;
        targetAngleZ += angle;
        if (targetAngleZ > angleCap) targetAngleZ = angleCap;
        if (targetAngleZ < -angleCap) targetAngleZ = -angleCap;
    }

    private void Update()
    {
        // Update target angle
        targetAngle = Quaternion.Euler(0, 0, targetAngleZ);

        if (targetAngleZ > 0)
        {
            targetAngleZ -= Time.deltaTime * swayDeceleration;
        } else if (targetAngleZ < 0)
        {
            targetAngleZ += Time.deltaTime * swayDeceleration;
        }

        SwayingSprite.transform.rotation = Quaternion.Lerp(SwayingSprite.transform.rotation, targetAngle, Time.deltaTime * lerpSpeed);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddSwayAngle(45);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddSwayAngle(-45);
        }
    }
}

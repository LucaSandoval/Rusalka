using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Preferences")] 
    
    [Header("General")]
    [SerializeField] private Animator anim;
    [SerializeField] private SpriteRenderer headSprite;
    private PlayerController playerController;
    //[SerializeField] private SpriteRenderer sprite;
    
    [Header("Swimming")]
    [SerializeField] private float turningSpeed = 250.0f;
    [SerializeField] private float maxTurnAngle = 130.0f;
    
    private float interpCurrent;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponentInParent<PlayerController>();
        interpCurrent = transform.up.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController == null) return;

        HandleDirection();
        HandleJump();
        HandleFloat();
        HandleSwimming();
    }

    private void HandleSwimming()
    {
        anim.SetBool("Swimming", playerController.IsInWater());
        
        if (!playerController.IsInWater())
        {
            interpCurrent = transform.up.z;
            return;
        }
        
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        // Difference between the character and direction we want to move in angles [-180, 180]
        float diff = Vector2.SignedAngle(transform.up, direction);
        
        // Rotate the character smoothly if the angle to rotate is smaller than certain angle threshold given by maxTurnAngle
        // Otherwise turn the character to the opposite direction and play Turning Animation
        if(Math.Abs(Math.Abs(diff) - 180.0f) > 180.0f - maxTurnAngle)
        {
            interpCurrent = Mathf.MoveTowards(interpCurrent, interpCurrent + diff, Time.deltaTime * turningSpeed);
        }
        else if(direction != Vector2.zero)
        {
            interpCurrent += diff;
            anim.SetTrigger("FireTurn");
        }
        
        // Apply new rotation
        transform.rotation = Quaternion.Euler(0, 0, interpCurrent);
        
        // Swimming Speed holds a value used as multiplayer for the playing speed of the Swim animation.
        anim.SetFloat("SwimmingSpeed", Mathf.Lerp(0.7f, 1.2f, Mathf.InverseLerp(0, playerController.GetMaxSwimmingSpeed(), playerController.GetSwimmingMovementVelocity())));
    }

    private void HandleFloat()
    {
        anim.SetBool("IsFloating", playerController.GetIsFloat());
    }

    private void HandleJump()
     {
         anim.SetFloat("VelocityY", playerController.GetMovementVelocity().y);
         anim.SetBool("IsGrounded", playerController.IsGrounded());
     }

    private void HandleDirection()
    {
        anim.SetFloat("Speed", playerController.GetMovementSpeed());
        if (playerController.Facing().x == 1)
        {
            headSprite.flipX = false;
        }
        else if (playerController.Facing().x == -1)
        {
            headSprite.flipX = true;
        }
    }
}

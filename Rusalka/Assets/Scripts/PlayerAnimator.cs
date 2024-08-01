using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Preferences")] [SerializeField]
    private Animator anim;

    [SerializeField] private SpriteRenderer headSprite;
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private float swimmingAnimationTurningSpeed = 300.0f;
    
    private float interpCurrent;
    
    private PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
        interpCurrent = transform.up.z;
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        HandleDirection();
        HandleJump();
        HandleFloat();
        HandleSwimming();
    }

    private float angle = 0.0f;
    private float diff = 0.0f; 

    private void HandleSwimming()
    {
        anim.SetBool("Swimming", player.IsInWater());

        if (!player.IsInWater())
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            return;
        }
        
        Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        float diff = Vector2.SignedAngle(transform.up, direction);
        //Debug.Log(diff);

        //float a = 180.0f - diff;

        if(Math.Abs(Math.Abs(diff) - 180.0f) > 10.0f)
        {
            interpCurrent = Mathf.MoveTowards(interpCurrent, interpCurrent + diff, Time.deltaTime * swimmingAnimationTurningSpeed); 
            //Debug.Log(interpCurrent);
        }
        else if(Mathf.Abs(Mathf.Abs(diff) - 180.0f) < 5.0f && direction != Vector2.zero)
        {
            //Debug.Log("here3");
            interpCurrent += 180.0f;
            anim.SetTrigger("FireTurn");
        }

        //interpCurrent = Mathf.MoveTowards(interpCurrent, diff, Time.deltaTime * swimmingAnimationTurningSpeed); 
        transform.rotation = Quaternion.Euler(0, 0, interpCurrent);
        
        anim.SetFloat("SwimmingSpeed", Mathf.Lerp(0.7f, 1.2f, Mathf.InverseLerp(0, player.GetMaxSwimmingSpeed(), player.GetSwimmingMovementVelocity())));
        //Debug.Log(direction + " " + directionVertical);
    }

    private void HandleFloat()
    {
        anim.SetBool("IsFloating", player.GetIsFloat());
    }

    private void HandleJump()
     {
         anim.SetFloat("VelocityY", player.GetMovementVelocity().y);
         anim.SetBool("IsGrounded", player.IsGrounded());
     }

    private void HandleDirection()
    {
        anim.SetFloat("Speed", player.GetMovementSpeed());
        if (player.Facing().x == 1)
        {
            headSprite.flipX = false;
        }
        else if (player.Facing().x == -1)
        {
            headSprite.flipX = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Preferences")] [SerializeField]
    private Animator anim;

    [SerializeField] private SpriteRenderer sprite;
    
    private PlayerController player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) return;

        HandleDirection();
        HandleJump();
        // HandleFloat();
        // HandleSwimming();
    }

    private void HandleSwimming()
    {
        if (player.IsInWater())
        {
            anim.SetBool("Swimming", true);
        }
        else
        {
            anim.SetBool("Swimming", false);
        }
    }

    private void HandleFloat()
    {
        if (player.GetIsFloat())
        {
            anim.SetBool("IsFloating", true);
        }
        else
        {
            anim.SetBool("IsFloating", false);
        }
    }

    private void HandleJump()
     {
         anim.SetFloat("VelocityY", player.GetHorizontalMovementSpeed());
         if (!player.IsGrounded())
         {
             anim.SetBool("Jump", true);
         }else
         {
             anim.SetBool("Jump", false);
         }
    }

    private void HandleDirection()
    {
        if (player.GetMovementSpeed() > 0.01f && player.IsGrounded())
        {
            anim.SetFloat("Speed", player.GetMovementSpeed());
        }
        else
        {
            anim.SetFloat("Speed", player.GetMovementSpeed());
        }
    }
}

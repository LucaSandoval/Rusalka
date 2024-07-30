using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [Header("Preferences")] [SerializeField]
    private Animator anim;

    [SerializeField] private SpriteRenderer headSprite;
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
        HandleFloat();
        HandleSwimming();
    }

    private void HandleSwimming()
    {
        float direction = Mathf.Clamp(Input.GetAxis("Horizontal"), -1.0f, 1.0f);
        if (direction != 0.0f)
        {
            transform.rotation = Quaternion.Euler(0, 0, -90.0f * direction);
        }else if ( Mathf.Abs(Input.GetAxis("Horizontal")) < 0.1f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        anim.SetBool("Swimming", player.IsInWater());
        //anim.SetFloat("SwimmingSpeed", );
        
    }

    private void HandleFloat()
    {
        anim.SetBool("IsFloating", player.GetIsFloat());
    }

    private void HandleJump()
     {
         anim.SetFloat("VelocityY", player.GetVerticalMovementVelocity());
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

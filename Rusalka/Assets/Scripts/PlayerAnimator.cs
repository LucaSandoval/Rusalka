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
        anim.SetBool("Swimming", player.IsInWater());

        if (!player.IsInWater())
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            return;
        }
        
        Vector2 direction = player.GetMovementVelocity().normalized;
        //float directionVertical = Mathf.Clamp(Input.GetAxis("Vertical"), -1.0f, 1.0f);
        if (direction.x >= 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.up, direction) * -1);
            Debug.Log(Vector2.Angle(Vector2.up, direction) * -1);
        }
        else if(direction.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.up, direction));
            Debug.Log(Vector2.Angle(Vector2.up, direction) + " Left");
        }

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

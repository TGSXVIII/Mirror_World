using System.Collections;
using UnityEngine;
using System.Threading.Tasks;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sprite;
    private BoxCollider2D coll;

    private float dirX = 0f;
    private int jumpCount = 0;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 18f;
    //[SerializeField] private AudioSource jumpSoundEffect;

    private enum MovementStatus { idle, running, jumping, falling }

    #endregion

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    }

    private async void Update()
    {
        await UpdatePlayerMovementAsync();
    }

    private async Task UpdatePlayerMovementAsync()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        // Normal jumping
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            jumpCount = 1;
            //jumpSoundEffect.Play();
            rb.velocity = new Vector2(0, jumpForce);
        }

        UpdateAnimation();
    }

    private void UpdateAnimation()
    {
        MovementStatus status;

        // Running
        if (dirX > 0f)
        {
            status = MovementStatus.running;
            sprite.flipX = false;
        }

        else if (dirX < 0f)
        {
            status = MovementStatus.running;
            sprite.flipX = true;
        }

        // Idle
        else
        {
            status = MovementStatus.idle;
        }

        // Jumping
        if (rb.velocity.y > .1f)
        {
            status |= MovementStatus.jumping;
        }

        // Falling
        else if (rb.velocity.y < -.1f)
        {
            status |= MovementStatus.falling;
        }

        //animator.SetInteger("status", (int)status);
    }

    private bool IsGrounded()
    {
        bool grounded = Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);

        if (grounded)
        {
            jumpCount = 0;
        }

        return grounded;
    }
}


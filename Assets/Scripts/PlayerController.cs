using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Mozgási Beállítások")]
    public float moveSpeed = 10f;
    public float jumpForce = 16f;
    [Range(0f, 1f)] public float jumpCutMultiplier = 0.5f;

    [Header("Időzítések (Pro Funkciók)")]
    public float coyoteTime = 0.2f;
    private float coyoteTimeCounter;

    public float jumpBufferTime = 0.1f;
    private float jumpBufferCounter;

    [Header("Dash & Slide")]
    public float dashForce = 24f;
    public float dashDuration = 0.2f;
    public float dashCooldown = 1f;
    private bool canDash = true;

    [Header("Referenciák & Fizika")]
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private Animator animator;

    private float horizontal;
    private bool isFacingRight = false;

    public enum PlayerState { Grounded, Airborne, Dashing }
    public PlayerState currentState = PlayerState.Airborne;

    void Start()
    {
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (IsGrounded())
        {
            coyoteTimeCounter = coyoteTime;
            if (currentState != PlayerState.Dashing) currentState = PlayerState.Grounded;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
            if (currentState != PlayerState.Dashing) currentState = PlayerState.Airborne;
        }

        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            Jump();
        }

        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpCutMultiplier);
            coyoteTimeCounter = 0f;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();

        // ANIMÁCIÓK FRISSÍTÉSE
        animator.SetFloat("xVelocity", Mathf.Abs(rb.linearVelocity.x));
        animator.SetFloat("yVelocity", rb.linearVelocity.y);
        animator.SetBool("isJumping", !IsGrounded());
    }

    void FixedUpdate()
    {
        if (PauseManager.isPaused) return;

        if (currentState == PlayerState.Dashing) return;

        if (DialogueManager.Instance != null && DialogueManager.Instance.isDialogueActive)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        jumpBufferCounter = 0f;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        currentState = PlayerState.Dashing;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(horizontal * dashForce, 0f);

        // Opcionális: Ha van Dash animációtok, itt lehet bekapcsolni: animator.SetBool("isDashing", true);

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        currentState = PlayerState.Airborne;
        // Opcionális: Itt pedig kikapcsolni: animator.SetBool("isDashing", false);

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
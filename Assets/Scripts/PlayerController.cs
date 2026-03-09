using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Mozgási Beállítások")]
    public float moveSpeed = 10f;
    public float jumpForce = 16f;
    [Range(0f, 1f)] public float jumpCutMultiplier = 0.5f; // Dinamikus ugrásmagassághoz

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

    private float horizontal;
    private bool isFacingRight = true;

    // Állapotgép (State Machine)
    public enum PlayerState { Grounded, Airborne, Dashing }
    public PlayerState currentState = PlayerState.Airborne;

    void Update()
    {
        // Bemenetek folyamatos figyelése
        horizontal = Input.GetAxisRaw("Horizontal");

        // 1. Coyote Time kezelése
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

        // 2. Input Buffering (Ugrás előkészítése)
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferCounter = jumpBufferTime;
        }
        else
        {
            jumpBufferCounter -= Time.deltaTime;
        }

        // 3. Ugrás végrehajtása (Coyote + Buffer kombinációja)
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            Jump();
        }

        // 4. Dinamikus ugrásmagasság (Ha elengeded a gombot, lassul az emelkedés)
        if (Input.GetButtonUp("Jump") && rb.linearVelocity.y > 0f)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpCutMultiplier);
            coyoteTimeCounter = 0f;
        }

        // 5. Dash indítása
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();
    }

    void FixedUpdate()
    {
        if (currentState == PlayerState.Dashing) return;

        // Alapmozgás
        rb.linearVelocity = new Vector2(horizontal * moveSpeed, rb.linearVelocity.y);
    }

    private void Jump()
    {
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        jumpBufferCounter = 0f; // Puffer ürítése ugrás után
    }

    private IEnumerator Dash()
    {
        canDash = false;
        currentState = PlayerState.Dashing;

        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f; // Gravitáció kikapcsolása a specifikáció szerint
        rb.linearVelocity = new Vector2(horizontal * dashForce, 0f);

        yield return new WaitForSeconds(dashDuration);

        rb.gravityScale = originalGravity;
        currentState = PlayerState.Airborne;

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
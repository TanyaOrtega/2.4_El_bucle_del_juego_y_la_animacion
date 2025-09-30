using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 12f;

    [Header("Ground Check")]
    public Transform groundCheck;             
    public float groundCheckRadius = 0.12f;
    public LayerMask groundLayer;             

    Rigidbody2D rb;
    Animator animator;
    bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Movimiento horizontal 
        float h = Input.GetAxisRaw("Horizontal");

        // Flip basado en dirección
        if (h > 0.01f) transform.localScale = new Vector3(1f, 1f, 1f);
        else if (h < -0.01f) transform.localScale = new Vector3(-1f, 1f, 1f);

        // Pasar velocidad horizontal a FixedUpdate usando rb.velocity allí
        Vector2 newVel = rb.linearVelocity;
        newVel.x = h * moveSpeed;
        rb.linearVelocity = newVel;

        // Animación: isWalking si hay entrada horizontal
        if (animator != null) animator.SetBool("isWalking", Mathf.Abs(h) > 0.01f);

        // Salto — solo si está en suelo
        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    bool IsGrounded()
    {
        if (groundCheck == null) return false;
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        if (animator != null) animator.SetBool("isGrounded", isGrounded);
        return isGrounded;
    }

    void OnDrawGizmosSelected()
    {
        if (groundCheck != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
        }
    }
}


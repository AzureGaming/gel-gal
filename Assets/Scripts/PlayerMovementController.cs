using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
    public Transform target;

    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteR;
    BoxCollider2D boxCollider;

    [SerializeField] float jumpSpeed = 1000f;
    bool grounded;
    bool shouldJump;
    float xMove;
    float moveSpeed = 2000f;
    Vector3 startScale;

    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        startScale = transform.localScale;
    }

    private void Update() {
        xMove = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            shouldJump = true;
            animator.SetTrigger("Jump");
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            animator.SetBool("Sprinting", true);
        } else {
            animator.SetBool("Sprinting", false);
        }

        Flip();
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }

    private void FixedUpdate() {
        if (shouldJump) {
            Jump();
        }
        IsGrounded();
        Move();
    }

    void IsGrounded() {
        LayerMask groundLayerMask = LayerMask.GetMask(GameManager.GROUNDING);
        float rayDistance = 5f;
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, rayDistance, groundLayerMask);
        if (hit.collider == null) {
            return;
        }

        float distance = Mathf.Abs(hit.point.y - target.position.y);
        if (distance < 0.08) {
            grounded = true;
            animator.SetBool("Grounded", grounded);
        } else {
            grounded = false;
            animator.SetBool("Grounded", grounded);
            animator.SetFloat("Falling", rb.velocity.y);
        }
    }

    void Flip() {
        if (Input.GetKeyDown(KeyCode.D)) {
            spriteR.flipX = false;
        } else if (Input.GetKeyDown(KeyCode.A)) {
            spriteR.flipX = true;
        }
    }

    void Jump() {
        grounded = false;
        shouldJump = false;
        Vector2 force = Vector2.up * jumpSpeed;
        Debug.Log("Jump force: " + force);
        rb.AddForce(force * Time.deltaTime, ForceMode2D.Impulse);
    }

    void Move() {
        float xForce = xMove * moveSpeed * Time.deltaTime;
        Vector2 force = new Vector2(xForce, 0);
        rb.AddForce(force);
    }
}

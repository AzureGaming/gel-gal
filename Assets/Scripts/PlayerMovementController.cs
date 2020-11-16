using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
    public Transform target;

    float jumpSpeed = 1000f;
    float moveSpeed = 2000f;
    float airMoveSpeed = 1500f;
    bool grounded;
    bool shouldJump;
    float xMove;
    float baseAirMoveSpeed;
    Vector3 startScale;

    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteR;
    BoxCollider2D boxCollider;

    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        startScale = transform.localScale;
        baseAirMoveSpeed = airMoveSpeed;
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
        animator.SetFloat("Vertical Velocity", rb.velocity.y);
    }

    private void FixedUpdate() {
        if (shouldJump) {
            Jump();
        }
        IsGrounded();
        Move();
    }

    private void OnEnable() {
        BounceArea.OnBounce += SlowAirMovement;
    }

    private void OnDisable() {
        BounceArea.OnBounce -= SlowAirMovement;
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
        } else {
            grounded = false;
        }
        animator.SetBool("Grounded", grounded);
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
        rb.AddForce(force * Time.deltaTime, ForceMode2D.Impulse);
    }

    void Move() {
        float xForce;
        if (!grounded) {
            xForce = xMove * airMoveSpeed;
        } else {
            xForce = xMove * moveSpeed;
        }
        Vector2 force = new Vector2(xForce * Time.deltaTime, 0);
        rb.AddForce(force);
    }

    void SlowAirMovement() {
        airMoveSpeed = 500f;
        StartCoroutine(ResetAirMovement());
    }

    IEnumerator ResetAirMovement() {
        yield return new WaitForSeconds(0.5f);
        airMoveSpeed = baseAirMoveSpeed;
    }
}

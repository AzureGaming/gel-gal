using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
    public Transform target;

    Animator animator;
    Rigidbody2D rb;

    [SerializeField] float jumpSpeed = 1000f;
    bool grounded;
    bool shouldJump;
    float xMove;
    float moveSpeed = 2000f;
    Vector3 startScale;

    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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

        if (Input.GetKeyDown(KeyCode.D)) {
            transform.localScale = startScale;
        } else if (Input.GetKeyDown(KeyCode.A)) {
            Vector3 newScale = startScale;
            newScale.x = -1;
            transform.localScale = newScale;
        }

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
        LayerMask layerMask = LayerMask.GetMask("Floor");
        RaycastHit2D hit = Physics2D.Raycast(target.position, -Vector2.up, Mathf.Infinity, layerMask);
        if (hit.collider == null) {
            return;
        }

        float distance = Mathf.Abs(hit.point.y - target.position.y);
        if (distance < 0.006) {
            grounded = true;
            animator.SetBool("Grounded", grounded);
        } else {
            grounded = false;
            animator.SetBool("Grounded", grounded);
            animator.SetFloat("Falling", rb.velocity.y);
        }
    }

    void Jump() {
        grounded = false;
        shouldJump = false;
        rb.AddForce(Vector2.up * jumpSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }

    void Move() {
        float xForce = xMove * moveSpeed * Time.deltaTime;
        Vector2 force = new Vector2(xForce, 0);
        rb.AddForce(force);
    }
}

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

    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            shouldJump = true;
            animator.SetTrigger("Jump");
        } else {
            animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        }
    }

    private void FixedUpdate() {
        if (shouldJump) {
            Jump();
        }
        IsGrounded();
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
        }
    }

    void Jump() {
        grounded = false;
        shouldJump = false;
        rb.AddForce(Vector2.up * jumpSpeed * Time.deltaTime, ForceMode2D.Impulse);
    }
}

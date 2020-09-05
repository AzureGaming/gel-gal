using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Rigidbody2D rb;

    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    bool isGrounded = true; 
    float xMove;
    bool shouldJump;

    private void Update() {
        xMove = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            shouldJump = true;
        };
    }

    private void FixedUpdate() {
        float xForce = xMove * moveSpeed * Time.deltaTime;
        Vector2 force = new Vector2(xForce, 0);
        rb.AddForce(force);

        if (shouldJump) {
            isGrounded = false;
            shouldJump = false;
            rb.AddForce(Vector2.up * jumpSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Floor") {
            isGrounded = true;
        }
    }
}

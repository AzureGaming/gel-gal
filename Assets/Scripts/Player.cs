using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public Rigidbody2D rb;
    public delegate void Shoot();
    public static event Shoot OnShoot;

    [SerializeField] float baseMoveSpeed = 6000f;
    [SerializeField] float sprintSpeed = 8000f;
    [SerializeField] float jumpSpeed = 1000f;

    SpriteRenderer spriteRenderer;

    float moveSpeed;
    float xMove;
    bool grounded = true;
    bool sprinting = false;
    bool jumping;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        xMove = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            jumping = true;
        }

        if (Input.GetMouseButtonDown(0)) {
            OnShoot?.Invoke();
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            sprinting = true;
        } else {
            sprinting = false;
        }

        FaceMouse();
    }

    private void FixedUpdate() {
        if (sprinting) {
            moveSpeed = sprintSpeed;
        } else {
            moveSpeed = baseMoveSpeed;
        }
        float xForce = xMove * moveSpeed * Time.deltaTime;
        Vector2 force = new Vector2(xForce, 0);
        rb.AddForce(force);

        if (jumping) {
            grounded = false;
            jumping = false;
            rb.AddForce(Vector2.up * jumpSpeed * Time.deltaTime, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.tag == "Floor") {
            grounded = true;
        }
    }

    void FaceMouse() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        Vector2 direction = mousePos - playerPos;

        if (direction.x > 0) {
            spriteRenderer.flipX = false;
        } else {
            spriteRenderer.flipX = true;
        }
    }
}

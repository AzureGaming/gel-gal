﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public delegate void Shoot(Vector2 direction);
    public static event Shoot OnShoot;
    public delegate void Aim(Vector2? direction);
    public static event Aim OnAim;

    public GameObject cratePrefab;

    [SerializeField] float baseMoveSpeed = 6000f;
    [SerializeField] float sprintSpeed = 8000f;
    [SerializeField] float jumpSpeed = 1000f;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    float moveSpeed;
    float xMove;
    float startGravityScale;
    bool grounded = true;
    bool sprinting = false;
    bool jumping;
    bool canPickUp = false;
    bool hasCrate = false;
    Vector2 direction;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        startGravityScale = rb.gravityScale;
    }

    private void OnEnable() {
        CratePickUp.OnPickUp += PickUpCrate;
    }

    private void OnDisable() {
        CratePickUp.OnPickUp -= PickUpCrate;
    }

    private void Update() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;

        xMove = Input.GetAxis("Horizontal");
        direction = mousePos - playerPos;

        if (Input.GetKeyDown(KeyCode.Space) && grounded) {
            jumping = true;
        }

        if (Input.GetMouseButton(1)) {
            OnAim?.Invoke(direction);
            if (Input.GetMouseButtonDown(0)) {
                OnShoot?.Invoke(direction);
            }
        } else {
            OnAim?.Invoke(null);
        }

        if (Input.GetKey(KeyCode.LeftShift)) {
            sprinting = true;
        } else {
            sprinting = false;
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            Debug.Log(hasCrate);
            if (hasCrate) {
                Instantiate(cratePrefab, transform.position, Quaternion.identity);
                hasCrate = false;
            }
        }

        FlipToFaceMouse();
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
        if (collision.collider.CompareTag(GameManager.FLOOR_TAG)) {
            grounded = true;
            rb.gravityScale = startGravityScale;
        }
    }

    public void CanPickup(bool value) {
        canPickUp = value;
    }

    void FlipToFaceMouse() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        Vector2 direction = mousePos - playerPos;

        if (direction.x > 0) {
            spriteRenderer.flipX = false;
        } else {
            spriteRenderer.flipX = true;
        }
    }

    void PickUpCrate() {
        StartCoroutine(CratePickUpCooldown());
    }

    IEnumerator CratePickUpCooldown() {
        yield return new WaitForSeconds(0.5f);
        hasCrate = true;
    }
}

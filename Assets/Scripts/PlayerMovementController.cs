﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour {
    public delegate void EmitJump();
    public static event EmitJump OnEmitJump;

    public delegate void Teleport(GameObject self, GameObject destination);
    public static Teleport OnTeleport;

    public BoxCollider2D boxCollider2d;
    public ParticleSystem dust;

    float jumpSpeed = 1000f;
    float moveSpeed = 2000f;
    float airMoveSpeed = 1500f;
    bool shouldJump;
    float xMove;
    float baseAirMoveSpeed;
    bool isGrounded;
    Vector3 startScale;
    GameObject teleportDestination;

    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteR;

    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
        startScale = transform.localScale;
        baseAirMoveSpeed = airMoveSpeed;
    }

    private void Update() {
        xMove = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
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
        EtherealArea.OnTeleportStart += StartTeleport;
        EtherealArea.OnTeleportEnd += EndTeleport;
    }

    private void OnDisable() {
        BounceArea.OnBounce -= SlowAirMovement;
        EtherealArea.OnTeleportStart -= StartTeleport;
        EtherealArea.OnTeleportEnd -= EndTeleport;
    }

    public void TeleportStartAnimationDone() {
        OnTeleport?.Invoke(gameObject, teleportDestination);
    }

    void IsGrounded() {
        float raycastPadding = 0.3f;
        LayerMask groundLayerMask = LayerMask.GetMask(GameManager.TerrainLayer, GameManager.SwitchLayer, GameManager.CratePlayerLayer);
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, raycastPadding, groundLayerMask);
        Color rayColor;

        if (raycastHit.collider == null) {
            rayColor = Color.red;
            isGrounded = false;
        } else {
            rayColor = Color.green;
            isGrounded = true;
            dust.Play();
        }
        animator.SetBool("Grounded", isGrounded);
        Debug.DrawRay(boxCollider2d.bounds.center, Vector2.down * (raycastPadding), rayColor);
    }

    void Flip() {
        if (Input.GetKeyDown(KeyCode.D)) {
            spriteR.flipX = false;
            dust.Play();
        } else if (Input.GetKeyDown(KeyCode.A)) {
            spriteR.flipX = true;
            dust.Play();
        }
    }

    void Jump() {
        dust.Play();
        shouldJump = false;
        Vector2 force = Vector2.up * jumpSpeed;
        rb.AddForce(force * Time.deltaTime, ForceMode2D.Impulse);
        animator.SetBool("Grounded", false);
        OnEmitJump?.Invoke();
    }

    void Ground(Collision2D collision) {
        animator.SetBool("Grounded", true);
    }

    void Move() {
        float xForce;
        if (!isGrounded) {
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

    void StartTeleport(GameObject objRef, GameObject destination) {
        if (objRef == gameObject) {
            teleportDestination = destination;
            animator.SetTrigger("Teleport");
            rb.isKinematic = true;
            rb.simulated = false;
        }
    }


    void EndTeleport(GameObject objRef, Action cb) {
        if (objRef == gameObject) {
            animator.SetTrigger("Teleport Done");
            teleportDestination = null;
            rb.isKinematic = false;
            rb.simulated = true;
        }
    }

    IEnumerator ResetAirMovement() {
        yield return new WaitForSeconds(0.5f);
        airMoveSpeed = baseAirMoveSpeed;
    }
}

using System;
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

    float BASE_AIR_MOVE_SPEED = 1500f;
    float BASE_MOVE_SPEED = 2000f;
    float BASE_SPRINT_SPEED = 4000f;
    float BASE_JUMP_SPEED = 1000f;
    float BOUNCED_AIR_SPEED = 500;
    float appliedMoveSpeed;
    float appliedAirMoveSpeed;
    bool shouldJump;
    float xMove;
    bool isGrounded;
    bool isSprinting;
    Vector3 startScale;
    GameObject teleportDestination;

    Animator animator;
    Rigidbody2D rb;
    SpriteRenderer spriteR;

    private void OnEnable() {
        BounceArea.OnBounce += ApplyBouncedAirMovement;
        EtherealArea.OnTeleportStart += StartTeleport;
        EtherealArea.OnTeleportEnd += EndTeleport;
    }

    private void OnDisable() {
        BounceArea.OnBounce -= ApplyBouncedAirMovement;
        EtherealArea.OnTeleportStart -= StartTeleport;
        EtherealArea.OnTeleportEnd -= EndTeleport;
    }

    private void Awake() {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        spriteR = GetComponent<SpriteRenderer>();
        appliedMoveSpeed = BASE_MOVE_SPEED;
        appliedAirMoveSpeed = BASE_AIR_MOVE_SPEED;
    }

    private void Start() {
        startScale = transform.localScale;
    }

    private void Update() {
        xMove = Input.GetAxis("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {
            shouldJump = true;
            animator.SetTrigger("Jump");
        }
        Flip();
        CheckSprint();
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetFloat("Vertical Velocity", rb.velocity.y);
    }

    private void FixedUpdate() {
        Jump();
        IsGrounded();
        Move();
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

    void CheckSprint() {
        isSprinting = Input.GetKey(KeyCode.LeftShift);
        if (isSprinting) {
            appliedMoveSpeed = BASE_SPRINT_SPEED;
        } else {
            appliedMoveSpeed = BASE_MOVE_SPEED;
        }
        animator.SetBool("Sprinting", isSprinting);
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
        if (shouldJump) {
            dust.Play();
            shouldJump = false;
            Vector2 force = Vector2.up * BASE_JUMP_SPEED;
            rb.AddForce(force * Time.deltaTime, ForceMode2D.Impulse);
            animator.SetBool("Grounded", false);
            OnEmitJump?.Invoke();
        }
    }

    void Move() {
        float xForce = xMove * (isGrounded ? appliedMoveSpeed : appliedAirMoveSpeed);
        Vector2 force = new Vector2(xForce * Time.deltaTime, 0);
        rb.AddForce(force);
    }

    void ApplyBouncedAirMovement() {
        appliedAirMoveSpeed = BOUNCED_AIR_SPEED;
        StartCoroutine(ResetAirMovement());
    }

    IEnumerator ResetAirMovement() {
        yield return new WaitForSeconds(0.5f);
        BOUNCED_AIR_SPEED = BASE_AIR_MOVE_SPEED;
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
}

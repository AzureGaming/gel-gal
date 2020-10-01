using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public delegate void Shoot(Vector2 direction, bool hasCrate);
    public static event Shoot OnShoot;
    public delegate void Aim(Vector2? direction);
    public static event Aim OnAim;

    public GameObject cratePrefab;
    public GameObject crateDropContainer;

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
    Vector3 startScale;
    float startDrag;
    float startAngularDrag;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        startGravityScale = rb.gravityScale;
        startScale = transform.localScale;
        startDrag = rb.drag;
        startAngularDrag = rb.angularDrag;
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
                OnShoot?.Invoke(direction, hasCrate);
                hasCrate = false;
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
            if (hasCrate) {
                Instantiate(cratePrefab, crateDropContainer.transform.position, Quaternion.identity);
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
            Debug.Log("Jump velocity" + rb.velocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag(GameManager.FLOOR_TAG) || collision.collider.CompareTag(GameManager.BUTTON_TRIGGER) || collision.collider.CompareTag(GameManager.CRATE)) {
            grounded = true;
            rb.gravityScale = startGravityScale;
            rb.drag = startDrag;
            rb.angularDrag = startAngularDrag;
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
            //spriteRenderer.flipX = false;
            transform.localScale = startScale;
        } else {
            //spriteRenderer.flipX = true;
            Vector3 newScale = startScale;
            newScale.x = -1;
            transform.localScale = newScale;
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

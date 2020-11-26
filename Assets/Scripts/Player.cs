using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public delegate void Death();
    public static event Death OnDeath;
    public delegate void Shoot(Vector2 direction, bool hasCrate, GameManager.GelType gelType);
    public static event Shoot OnShoot;
    public delegate void Aim(Vector2? direction);
    public static event Aim OnAim;

    public GameObject cratePrefab;
    public GameObject crateDropContainer;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;

    bool sprinting = false;
    bool canPickUp = false;
    bool hasCrate = false;
    [SerializeField] GameManager.GelType equippedGel;
    Vector2 direction;
    Vector3 startScale;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        equippedGel = GameManager.GelType.Ethereal;
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

        direction = mousePos - playerPos;

        if (Input.GetMouseButton(1)) {
            OnAim?.Invoke(direction);
            if (Input.GetMouseButtonDown(0)) {
                animator.SetTrigger("Shoot");
                StartCoroutine(DelayShoot());
            }
        } else {
            OnAim?.Invoke(null);
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            if (hasCrate) {
                GameObject crateObj = Instantiate(cratePrefab, crateDropContainer.transform.position, Quaternion.identity);
                crateObj.GetComponent<Rigidbody2D>().velocity = rb.velocity;
                hasCrate = false;
            }
        }
    }

    IEnumerator DelayShoot() {
        yield return new WaitForSeconds(0.25f);
        OnShoot?.Invoke(direction, hasCrate, equippedGel);
        hasCrate = false;
    }

    public void CanPickup(bool value) {
        canPickUp = value;
    }

    void Die() {
        OnDeath?.Invoke();
    }

    void PickUpCrate() {
        StartCoroutine(CratePickUpCooldown());
    }

    IEnumerator CratePickUpCooldown() {
        yield return new WaitForSeconds(0f);
        hasCrate = true;
    }
}

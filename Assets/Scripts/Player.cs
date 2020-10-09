using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    public delegate void Death();
    public static event Death OnDeath;
    public delegate void Shoot(Vector2 direction, bool hasCrate);
    public static event Shoot OnShoot;
    public delegate void Aim(Vector2? direction);
    public static event Aim OnAim;

    public GameObject cratePrefab;
    public GameObject crateDropContainer;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;

    bool sprinting = false;
    bool canPickUp = false;
    bool hasCrate = false;
    Vector2 direction;
    Vector3 startScale;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
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
                OnShoot?.Invoke(direction, hasCrate);
                hasCrate = false;
            }
        } else {
            OnAim?.Invoke(null);
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            if (hasCrate) {
                Instantiate(cratePrefab, crateDropContainer.transform.position, Quaternion.identity);
                hasCrate = false;
            }
        }

        //FlipToFaceMouse();
    }

    public void CanPickup(bool value) {
        canPickUp = value;
    }

    void FlipToFaceMouse() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        Vector2 direction = mousePos - playerPos;

        if (direction.x > 0) {
            transform.localScale = startScale;
        } else {
            Vector3 newScale = startScale;
            newScale.x = -1;
            transform.localScale = newScale;
        }
    }

    void Die() {
        OnDeath?.Invoke();
    }

    void PickUpCrate() {
        StartCoroutine(CratePickUpCooldown());
    }

    IEnumerator CratePickUpCooldown() {
        yield return new WaitForSeconds(0.5f);
        hasCrate = true;
    }
}

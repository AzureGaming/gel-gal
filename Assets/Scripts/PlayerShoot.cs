using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour {
    public delegate void Aim(Vector2? direction, float launchForce, float gravityScale);
    public static event Aim OnAim;

    public GameObject bounceGelPrefab;
    public GameObject etherealGelPrefab;
    public GameObject cratePrefab;
    public GameObject projectileOrigin;
    public GameObject crateOrigin;

    float GEL_FORCE = 4f;
    float CRATE_FORCE = 2f;
    bool releaseCheck = false;

    Player player;
    Animator animator;
    Vector2 direction;
    Vector2 lastDirection;

    private void Awake() {
        animator = GetComponent<Animator>();
        player = GetComponent<Player>();
    }

    private void Update() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;

        direction = mousePos - playerPos;

        if (Input.GetMouseButton(1)) {
            float force = player.HasCrate() ? CRATE_FORCE : GEL_FORCE;
            float gravityScale = player.HasCrate() ? cratePrefab.GetComponent<Rigidbody2D>().gravityScale : bounceGelPrefab.GetComponent<Rigidbody2D>().gravityScale;
            OnAim?.Invoke(direction, force, gravityScale);
            if (Input.GetMouseButtonDown(0)) {
                animator.SetTrigger("Shoot");
                StartCoroutine(DelayShoot());
            }
        } else {
            OnAim?.Invoke(null, 0, 0);
        }
    }

    IEnumerator DelayShoot() {
        yield return new WaitForSeconds(0.25f);
        SpawnProjectile(direction, player.HasCrate(), player.GetEquippedGel());
        player.SetHasCrate(false);
    }

    void SpawnProjectile(Vector2 direction, bool hasCrate, GameManager.GelType gelType) {
        lastDirection = direction;
        if (hasCrate) {
            SpawnCrateProjectile();
        } else if (gelType == GameManager.GelType.Bounce) {
            SpawnGelProjectile(bounceGelPrefab);
        } else if (gelType == GameManager.GelType.Ethereal) {
            SpawnGelProjectile(etherealGelPrefab);
        }
    }

    void SpawnGelProjectile(GameObject gelPrefab) {
        GameObject gelRef = Instantiate(gelPrefab, transform.position, transform.rotation);
        Vector3 velocity = projectileOrigin.transform.right * (lastDirection.magnitude * GEL_FORCE);
        gelRef.GetComponent<Rigidbody2D>().velocity = velocity;
    }

    void SpawnCrateProjectile() {
        GameObject crateRef = Instantiate(cratePrefab, transform.position, transform.rotation);
        Vector3 velocity = projectileOrigin.transform.right * (lastDirection.magnitude * CRATE_FORCE);
        crateRef.GetComponent<Rigidbody2D>().velocity = velocity;
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour {
    public delegate void Death();
    public static event Death OnDeath;

    public GameObject cratePrefab;
    public GameObject crateDropContainer;

    SpriteRenderer spriteRenderer;
    Rigidbody2D rb;
    Animator animator;

    bool sprinting = false;
    bool canPickUp = false;
    bool hasCrate = false;
    [SerializeField] GameManager.GelType equippedGel;
    List<GameManager.GelType> availableGelsToEquip = new List<GameManager.GelType>();
    Vector3 startScale;

    private void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        availableGelsToEquip.Add(GameManager.GelType.Bounce);
        availableGelsToEquip.Add(GameManager.GelType.Ethereal);
        equippedGel = availableGelsToEquip[0];
    }

    private void OnEnable() {
        CratePickUp.OnPickUp += PickUpCrate;
    }

    private void OnDisable() {
        CratePickUp.OnPickUp -= PickUpCrate;
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Tab)) {
            equippedGel = availableGelsToEquip.SkipWhile(x => x != equippedGel).Skip(1).DefaultIfEmpty(availableGelsToEquip[0]).FirstOrDefault();
        }

        if (Input.GetKeyDown(KeyCode.F)) {
            if (hasCrate) {
                GameObject crateObj = Instantiate(cratePrefab, crateDropContainer.transform.position, Quaternion.identity);
                crateObj.GetComponent<Rigidbody2D>().velocity = rb.velocity;
                hasCrate = false;
            }
        }
    }

    public void CanPickup(bool value) {
        canPickUp = value;
    }

    public bool HasCrate() {
        return hasCrate;
    }

    public void SetHasCrate(bool value) {
        hasCrate = value;
    }

    public GameManager.GelType GetEquippedGel() {
        return equippedGel;
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

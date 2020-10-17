using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
    public delegate void ButtonActivated(bool activated);
    public static event ButtonActivated OnButtonActivate;

    public Sprite upSprite;
    public Sprite downSprite;
    public Vector2 colliderSizeUp;
    public Vector2 colliderSizeDown;

    public SpriteRenderer spriteR;

    CapsuleCollider2D capsuleCollider;

    private void Awake() {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (IsValidTrigger(collision)) {
            OnButtonActivate?.Invoke(true);
            ButtonDown();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (IsValidTrigger(collision)) {
            OnButtonActivate?.Invoke(false);
            ButtonUp();
        }
    }

    public void ButtonUp() {
        spriteR.sprite = upSprite;
        capsuleCollider.size = colliderSizeUp;
    }

    public void ButtonDown() {
        spriteR.sprite = downSprite;
        capsuleCollider.size = colliderSizeDown;
    }

    bool IsValidTrigger(Collider2D collision) {
        if (collision.CompareTag(GameManager.PLAYER_TAG) || collision.CompareTag(GameManager.CRATE)) {
            return true;
        }
        return false;
    }
}

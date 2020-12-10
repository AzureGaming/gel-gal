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

    SpriteRenderer spriteR;
    CapsuleCollider2D capsuleCollider;

    protected bool isActivated = false;

    private void Awake() {
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        spriteR = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (!isActivated) {
            OnButtonActivate?.Invoke(true);
            isActivated = true;
            ButtonDown();
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
    
    protected virtual void OnButtonActivated(bool activated) {
        OnButtonActivate?.Invoke(activated);
    }
}

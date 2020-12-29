using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureVelocity : MonoBehaviour {
    public Vector2 lastVelocity;

    private void OnEnable() {
        BounceArea.OnBounce += Reset;
    }

    private void OnDisable() {
        BounceArea.OnBounce -= Reset;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GameManager.PLAYER_TAG) || collision.CompareTag(GameManager.CRATE)) {
            Vector2 velocity = collision.attachedRigidbody.velocity;
            if (velocity.magnitude > 0) {
                lastVelocity = velocity;
            }
        }
    }

    private void Reset() {
        lastVelocity = Vector2.zero;
    }
}

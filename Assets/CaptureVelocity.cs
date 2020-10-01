using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaptureVelocity : MonoBehaviour {
    public Vector2 lastVelocity;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GameManager.PLAYER_TAG)) {
            Vector2 velocity = collision.attachedRigidbody.velocity;
            if (velocity.magnitude > 0) {
                lastVelocity = velocity;

            }
            Debug.Log("Store" + lastVelocity);
        }
    }
}

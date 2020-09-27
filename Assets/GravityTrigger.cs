using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTrigger : MonoBehaviour {
    public Vector2 lastVelocity;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GameManager.PLAYER_TAG)) {
            lastVelocity = collision.attachedRigidbody.velocity;
            collision.attachedRigidbody.gravityScale = 0;
        }
    }
}

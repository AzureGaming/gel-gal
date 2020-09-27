using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityTrigger : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GameManager.PLAYER_TAG)) {
            collision.attachedRigidbody.gravityScale = 0;
        }
    }
}

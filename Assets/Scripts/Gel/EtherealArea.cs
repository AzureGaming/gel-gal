using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherealArea : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        BoxCollider2D collider = collision.GetComponent<BoxCollider2D>();

        if (!collider) {
            return;
        }
        if (collision.CompareTag(GameManager.CRATE)) {
            collider.enabled = false;
        } else {
            Player player = collision.GetComponentInParent<Player>();
            if (player) {
                player.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}

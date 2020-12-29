using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardTileMap : MonoBehaviour {
    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag(GameManager.PLAYER_TAG)) {
            Debug.Log("Kill player");
        }
    }
}

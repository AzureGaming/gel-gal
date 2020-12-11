using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrateCollider : MonoBehaviour {
    public BoxCollider2D boxCollider2d;

    private void OnTriggerEnter2D(Collider2D collision) {
        boxCollider2d.enabled = false;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        boxCollider2d.enabled = true;
    }
}

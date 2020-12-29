using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideWithCrate : MonoBehaviour {
    public BoxCollider2D boxCollider2d;

    void EnableCollider() {
        boxCollider2d.enabled = true;
    }
    void DisableCollider() {
        boxCollider2d.enabled = false;
    }
}

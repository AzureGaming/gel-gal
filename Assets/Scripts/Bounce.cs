using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {
    [SerializeField] float bounceFactor = 4000f;
    [SerializeField] float gravityApplied = 50f;

    private void OnCollisionEnter2D(Collision2D collision) {
        Rigidbody2D rb = collision.collider.attachedRigidbody;
        if (!rb) {
            return;
        }
        Vector2 force = new Vector2(0, bounceFactor * Time.deltaTime);
        rb.gravityScale = gravityApplied;
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}

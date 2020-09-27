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

        StartCoroutine(EnableGravity(rb));
        //rb.drag = 0;
        //rb.angularDrag = 0;
        //rb.gravityScale = 0;
        //rb.angularVelocity = 0;

        //float speed = rb.velocity.magnitude;
        //Vector2 direction = Vector2.Reflect(rb.velocity.normalized, collision.contacts[0].normal);
        //rb.velocity = Vector2.zero;
        //rb.AddForce(direction * Mathf.Max(speed * 20, 0f));
    }

    IEnumerator EnableGravity(Rigidbody2D rb) {
        yield return new WaitForSeconds(0.15f);
        rb.gravityScale = 5;
    }
}

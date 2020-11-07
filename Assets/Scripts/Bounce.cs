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

        foreach (ContactPoint2D contact in collision.contacts) {
            float speed = 2f;
            Vector2 lastVelocity = GetComponentInChildren<CaptureVelocity>().lastVelocity;
            Quaternion bounceAngle = Quaternion.AngleAxis(180, contact.normal);
            Vector2 direction = Vector2.Reflect(lastVelocity, contact.normal);

            if (transform.rotation.eulerAngles.z == 0 || transform.rotation.eulerAngles.z == 180) {
                // area is on the ground or ceiling
                direction.y = Mathf.Max(10f, direction.y) * Mathf.Sign(direction.y);
                if (Mathf.Sign(lastVelocity.x) > 0) { // moving right
                    direction.x = Mathf.Clamp(lastVelocity.x, 1, 5);
                } else {
                    direction.x = Mathf.Clamp(lastVelocity.x, -5, -1);
                }
            } else {
                direction.x = 7f * Mathf.Sign(direction.x);
                direction.y = 14f * Mathf.Sign(direction.y);
            }
            rb.velocity = direction * speed;
        }
    }
}

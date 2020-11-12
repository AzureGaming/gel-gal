using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceArea : MonoBehaviour {
    [SerializeField] float bounceFactor = 4000f;
    [SerializeField] float gravityApplied = 50f;
    public delegate void Bounce();
    public static event Bounce OnBounce;

    private void OnCollisionEnter2D(Collision2D collision) {
        Rigidbody2D rb = collision.collider.attachedRigidbody;
        if (!rb) {
            return;
        }

        foreach (ContactPoint2D contact in collision.contacts) {
            float speed = 1.75f;
            Vector2 lastVelocity = GetComponentInChildren<CaptureVelocity>().lastVelocity;
            Vector2 direction = Vector2.Reflect(lastVelocity, contact.normal);

            if (transform.rotation.eulerAngles.z == 0 || transform.rotation.eulerAngles.z == 180) {
                // area is on the ground or ceiling
                direction.y = direction.y * Mathf.Sign(direction.y);
                if (Mathf.Sign(lastVelocity.x) > 0) { // moving right
                    direction.x = Mathf.Clamp(lastVelocity.x, 1, 5);
                } else {
                    direction.x = Mathf.Clamp(lastVelocity.x, -5, -1);
                }
            } else {
                direction.x = 10f * Mathf.Sign(direction.x);
                direction.y = 14f * Mathf.Sign(direction.y);
            }

            // Ensuring the object bounces back to the peak of it's original arc is difficult
            // Ref: https://answers.unity.com/questions/854006/jumping-a-specific-height-using-velocity-gravity.html
            rb.velocity = Vector2.zero;
            Vector2 newVelocity = lastVelocity;
            newVelocity.y = Mathf.Max(20f, direction.y * speed);
            newVelocity.x = direction.x;
            rb.velocity = newVelocity;
            OnBounce?.Invoke();
        }
    }
}

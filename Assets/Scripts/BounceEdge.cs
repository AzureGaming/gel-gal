using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceEdge : MonoBehaviour {
    [SerializeField] float bounceFactor = 4000f;
    [SerializeField] float gravityApplied = 50f;
    public bool toggle = false;

    float speed = 10f;

    private void OnCollisionEnter2D(Collision2D collision) {
        Rigidbody2D rb = collision.collider.attachedRigidbody;
        bool isValidCollision = GetComponentInParent<BounceArea>().hasCollided;

        if (!rb || !isValidCollision) {
            return;
        }
        foreach (ContactPoint2D contact in collision.contacts) {
            Vector2 lastVelocity = GetComponentInChildren<CaptureVelocity>().lastVelocity;
            Vector3 newVelocity;

            if (transform.rotation.eulerAngles.z == 0 || transform.rotation.eulerAngles.z == 180) {
                newVelocity = transform.right * speed;
            } else {
                newVelocity = transform.up * speed;
            }
            if (toggle) {
                newVelocity = -newVelocity;
            }

            rb.velocity = newVelocity;
        }
    }
}

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
            Vector2 lastVelocity = GetComponentInChildren<GravityTrigger>().lastVelocity;
            Quaternion bounceAngle = Quaternion.AngleAxis(180, contact.normal);
            Vector2 direction = Vector2.Reflect(lastVelocity, contact.normal);

            Debug.Log("Direction: " + direction);
            Debug.Log("rotation: " + transform.rotation.eulerAngles);
            if (transform.rotation.eulerAngles.z == 0 || transform.rotation.eulerAngles.z == 180) {
                // area is on the ground or ceiling
                Debug.Log("Area on ground or celing");

            } else {
                if (direction.x < 0) {
                    // Bounce left
                    direction.x = -9f;
                    direction.y = 9f;
                } else {
                    // Bounce right
                    direction.x = 9f;
                    direction.y = 9f;
                }
                rb.velocity = direction * speed;
            }

            Debug.Log("bounce velocity" + rb.velocity);
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

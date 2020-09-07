using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gel : MonoBehaviour {
    public GameObject collisionParticlesPrefab;
    public GameObject bounceAreaPrefab;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag(GameManager.FLOOR_TAG)) {
            Vector2 collisionPos = new Vector3(collision.GetContact(0).point.x, collision.GetContact(0).point.y);

            Destroy(gameObject);
            Instantiate(collisionParticlesPrefab, transform.position, Quaternion.identity);
            Instantiate(bounceAreaPrefab, collisionPos, Quaternion.identity);
        }
    }
}

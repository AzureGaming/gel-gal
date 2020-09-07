using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gel : MonoBehaviour {
    public GameObject collisionParticlesPrefab;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag(GameManager.FLOOR_TAG)) {
            Instantiate(collisionParticlesPrefab, transform.position, Quaternion.identity);
        }
    }
}

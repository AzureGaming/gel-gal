using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour {
    public GameObject bounceAreaPrefab;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag(GameManager.GEL_TAG)) {
            Destroy(collision.collider.gameObject);
            Vector2 collisionPos = new Vector3(collision.GetContact(0).point.x, collision.GetContact(0).point.y);
            Instantiate(bounceAreaPrefab, collisionPos, Quaternion.identity);
        }
    }
}

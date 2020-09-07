using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GameManager.GEL_TAG)) {
            Destroy(collision.gameObject);
        }
    }
}

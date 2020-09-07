using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bound : MonoBehaviour {
    string GEL_TAG = "Gel";

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GEL_TAG)) {
            Destroy(collision.gameObject);
        }
    }
}

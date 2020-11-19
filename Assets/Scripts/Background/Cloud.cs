using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {
    public GameObject bounds;

    void Update() {
        Vector3 newPos = transform.position;
        newPos.x -= 0.1f;
        transform.position = newPos;

        if (!IsWithinBounds()) {
            Destroy(gameObject);
        }
    }

    bool IsWithinBounds() {
        float maxX = bounds.transform.position.x + bounds.transform.localScale.x / 2;
        float maxY = bounds.transform.position.y + bounds.transform.localScale.y / 2;
        float minX = -maxX;
        float minY = bounds.transform.position.y - bounds.transform.localScale.y / 2;

        if (transform.position.x > maxX || transform.position.x < minX) {
            return false;
        }

        if (transform.position.y > maxY || transform.position.y < minY) {
            return false;
        }

        return true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : MonoBehaviour {
    CloudSpawnArea spawnArea;

    private void Awake() {
        spawnArea = GetComponentInParent<CloudLayer>().spawnArea;
    }

    void Update() {
        Vector3 newPos = transform.position;
        newPos.x -= 0.003f;
        transform.position = newPos;

        if (!IsWithinBounds()) {
            Destroy(gameObject);
        }
    }

    bool IsWithinBounds() {
        if (transform.position.x < spawnArea.minX) {
            return false;
        }

        if (transform.position.y < spawnArea.minY) {
            return false;
        }

        return true;
    }
}

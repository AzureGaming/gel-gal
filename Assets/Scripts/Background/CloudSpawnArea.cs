using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawnArea : MonoBehaviour {
    public float maxX;
    public float maxY;
    public float minX;
    public float minY;

    private void Start() {
        maxX = transform.position.x + transform.localScale.x / 2;
        maxY = transform.position.y + transform.localScale.y / 2;
        minX = -maxX;
        minY = maxY - transform.localScale.y;
    }
}

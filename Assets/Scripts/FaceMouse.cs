using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceMouse : MonoBehaviour {
    void Update() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 objPos = transform.position;
        Vector2 direction = mousePos - objPos;
        transform.right = direction;
    }
}

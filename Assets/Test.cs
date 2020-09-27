using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
    Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start() {
        Vector2 force = Vector2.right;
        force.y = 1f;
        rb.AddForce(force * 2, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update() {

    }
}

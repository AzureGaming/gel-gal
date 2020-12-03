using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ethereal : MonoBehaviour {
    public delegate void Teleport(Transform transform);
    public static event Teleport OnTeleport;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GameManager.ETHEREAL_AREA)) {
            collision.attachedRigidbody.velocity = Vector2.zero;
            OnTeleport?.Invoke(collision.transform);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ethereal : MonoBehaviour {
    public delegate void Teleport(GameObject gameObj, GameObject destination);
    public static event Teleport OnTeleport;

    public BoxCollider2D collider2d;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GameManager.ETHEREAL_AREA)) {
            OnTeleport?.Invoke(gameObject, collision.gameObject);
            StartCoroutine(StartTriggerCooldown());
        }
    }

    IEnumerator StartTriggerCooldown() {
        collider2d.enabled = false;
        yield return new WaitForSeconds(0.5f);
        collider2d.enabled = true;
    }
}

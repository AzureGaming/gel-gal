using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceGel : Gel {
    private void OnCollisionEnter2D(Collision2D collision) {
        //if (collision.collider.CompareTag(GameManager.TERRAIN)) {
            ContactPoint2D contact = collision.GetContact(0);
            SpawnArea(contact.point.x, contact.point.y, contact.normal);
        //}
    }
}

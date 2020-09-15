﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Gel : MonoBehaviour {
    public GameObject collisionParticlesPrefab;
    public GameObject bounceAreaPrefab;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag(GameManager.FLOOR_TAG)) {
            ContactPoint2D contact = collision.GetContact(0);
            Destroy(gameObject);
            SpawnBounceArea(contact.point.x, contact.point.y, contact.normal);
            Instantiate(collisionParticlesPrefab, transform.position, Quaternion.identity);
        }
    }

    void SpawnBounceArea(float xPos, float yPos, Vector3 normal) {
        Vector3 collisionPos = new Vector3(xPos, yPos);
        Quaternion newRot = Quaternion.FromToRotation(Vector2.up, normal);
        Vector3 hitPos = Vector3.zero;
        hitPos.x = xPos - 0.01f * normal.x;
        hitPos.y = yPos - 0.01f * normal.y;

        Instantiate(bounceAreaPrefab, hitPos, newRot);
    }
}

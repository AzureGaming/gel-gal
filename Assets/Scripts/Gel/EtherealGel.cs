using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EtherealGel : Gel {
    public delegate void TileCollision(GameObject etherealAreaRef, Collision2D collision);
    public static event TileCollision OnTileCollision;

    TileManager tileManager;

    private void Awake() {
        tileManager = FindObjectOfType<TileManager>();
    }

    private void OnEnable() {
        TileManager.OnSpawnEtherealAreaEnd += SpawnArea;
    }

    private void OnDisable() {
        TileManager.OnSpawnEtherealAreaEnd -= SpawnArea;
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Vector3 hitPosition = Vector3.zero;
        if (tileManager.IsValidCollision(collision.gameObject)) {
            ContactPoint2D contact = collision.GetContact(0);
            GameObject instance = SpawnArea(contact.point.x, contact.point.y, contact.normal);
            OnTileCollision?.Invoke(instance, collision);
        } else {
            SpawnCollisionParticles();
            Despawn();
        }
    }
}

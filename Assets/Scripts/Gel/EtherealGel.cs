using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EtherealGel : Gel {
    public GameObject tilemapGameObject;
    Tilemap tilemap;

    public GameObject tempTileMapGameObject;
    Tilemap tempTilemap;

    private void Start() {
        tilemapGameObject = GameObject.FindGameObjectWithTag("Floor");
        if (tilemapGameObject != null) {
            tilemap = tilemapGameObject.GetComponent<Tilemap>();
        }

        tempTileMapGameObject = GameObject.FindGameObjectWithTag("Temp");
        if (tempTileMapGameObject != null) {
            tempTilemap = tempTileMapGameObject.GetComponent<Tilemap>();
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        Vector3 hitPosition = Vector3.zero;
        if (tilemap != null && tilemapGameObject == collision.gameObject) {
            Vector3Int tilePos = new Vector3Int();
            TileBase originalTile = null;
            ContactPoint2D contact = collision.GetContact(0);

            hitPosition.x = contact.point.x - 0.01f * contact.normal.x;
            hitPosition.y = contact.point.y - 0.01f * contact.normal.y;
            originalTile = tilemap.GetTile(tilemap.WorldToCell(hitPosition));
            tilePos = tilemap.WorldToCell(hitPosition);

            tilemap.SetTileFlags(tilePos, TileFlags.None);
            tilemap.SetTile(tilePos, null);
            tempTilemap.SetTile(tilePos, originalTile);

            SpawnArea(contact.point.x, contact.point.y, contact.normal);
        }
    }
}

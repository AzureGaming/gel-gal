using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour {
    public GameObject tilemapGameObject;
    Tilemap tilemap;

    public GameObject tempTileMapGameObject;
    Tilemap tempTilemap;

    Vector3Int etherealTilePos;

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

    private void OnEnable() {
        EtherealGel.OnTileCollision += DisableTile;
    }

    private void OnDisable() {
        EtherealGel.OnTileCollision -= DisableTile;
    }

    public Vector3Int GetEtherealTilePos() {
        return etherealTilePos;
    }

    public bool IsValidCollision(GameObject objCollidedWith) {
        if (objCollidedWith == tilemapGameObject) {
            return true;
        }
        return false;
    }

    void DisableTile(Collision2D collision) {
        ContactPoint2D contact = collision.GetContact(0);
        Vector3 hitPosition = Vector3.zero;
        TileBase tile = null;
        Vector3Int tilePos = new Vector3Int();

        hitPosition.x = contact.point.x - 0.01f * contact.normal.x;
        hitPosition.y = contact.point.y - 0.01f * contact.normal.y;
        tile = tilemap.GetTile(tilemap.WorldToCell(hitPosition));
        tilePos = tilemap.WorldToCell(hitPosition);

        tilemap.SetTileFlags(tilePos, TileFlags.None);
        tilemap.SetTile(tilePos, null);
        tempTilemap.SetTile(tilePos, tile);

        etherealTilePos = tilePos;
    }

}

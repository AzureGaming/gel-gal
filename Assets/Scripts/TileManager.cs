﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour {
    public GameObject tilemapGameObject;
    Tilemap tilemap;

    public GameObject tempTileMapGameObject;
    Tilemap tempTilemap;

    List<Vector3Int> etherealTilePositions;
    List<TileBase> etherealTiles;

    private void Awake() {
        etherealTilePositions = new List<Vector3Int>();
        etherealTiles = new List<TileBase>();
    }

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
        EtherealArea.OnRemove += EnableTile;
    }

    private void OnDisable() {
        EtherealGel.OnTileCollision -= DisableTile;
        EtherealArea.OnRemove -= EnableTile;
    }

    public bool IsValidCollision(GameObject objCollidedWith) {
        if (objCollidedWith == tilemapGameObject) {
            return true;
        }
        return false;
    }

    void EnableTile() {
        TileBase tile = etherealTiles[0];
        Vector3Int pos = etherealTilePositions[0];

        tempTilemap.SetTile(pos, null);
        tilemap.SetTile(pos, tile);

        etherealTiles.Remove(tile);
        etherealTilePositions.Remove(pos);
    }

    void DisableTile(Collision2D collision) {
        ContactPoint2D contact = collision.GetContact(0);
        Vector3 hitPosition = Vector3.zero;
        Vector3Int tilePos = Vector3Int.zero;
        TileBase tile;

        hitPosition.x = contact.point.x - 0.01f * contact.normal.x;
        hitPosition.y = contact.point.y - 0.01f * contact.normal.y;
        tile = tilemap.GetTile(tilemap.WorldToCell(hitPosition));
        tilePos = tilemap.WorldToCell(hitPosition);

        tilemap.SetTileFlags(tilePos, TileFlags.None);
        tempTilemap.SetTileFlags(tilePos, TileFlags.None);
        tilemap.SetTile(tilePos, null);
        tempTilemap.SetTile(tilePos, tile);

        etherealTilePositions.Add(tilePos);
        etherealTiles.Add(tile);
    }

}
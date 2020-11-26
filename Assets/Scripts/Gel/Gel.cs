using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Gel : MonoBehaviour {
    public delegate void SpawnGelArea(GameObject gelAreaRef, int type);
    public static event SpawnGelArea OnSpawnGelArea;

    public GameObject collisionParticlesPrefab;
    public GameObject gelAreaPrefab;

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


    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.collider.CompareTag(GameManager.FLOOR_TAG)) {
            ContactPoint2D contact = collision.GetContact(0);
            SpawnArea(contact.point.x, contact.point.y, contact.normal);
            Instantiate(collisionParticlesPrefab, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }

    void SpawnArea(float xPos, float yPos, Vector3 normal) {
        Vector3 collisionPos = new Vector3(xPos, yPos);
        Quaternion newRot = Quaternion.FromToRotation(Vector2.up, normal);
        Vector3 hitPos = Vector3.zero;
        hitPos.x = xPos - 0.01f * normal.x;
        hitPos.y = yPos - 0.01f * normal.y;

        GameObject instance = Instantiate(gelAreaPrefab, hitPos, newRot);
        OnSpawnGelArea?.Invoke(instance, 0);
    }

    //void OnCollisionEnter2D(Collision2D collision) {
    //    Vector3 hitPosition = Vector3.zero;
    //    if (tilemap != null && tilemapGameObject == collision.gameObject) {
    //        foreach (ContactPoint2D hit in collision.contacts) {
    //            hitPosition.x = hit.point.x - 0.01f * hit.normal.x;
    //            hitPosition.y = hit.point.y - 0.01f * hit.normal.y;
    //            TileBase originalTile = tilemap.GetTile(tilemap.WorldToCell(hitPosition));
    //            Vector3Int tilePos = tilemap.WorldToCell(hitPosition);
    //            tempTilemap.SetTile(tilePos, originalTile);
    //            tilemap.SetTileFlags(tilePos, TileFlags.None);
    //            tilemap.SetColor(tilePos, Color.clear);
    //            tilemap.SetTile(tilePos, null);
    //        }
    //    }
    //}
}

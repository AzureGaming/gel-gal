using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour {
    public delegate GameObject SpawnEtherealAreaEnd(float x, float y, Vector3 normal);
    public static event SpawnEtherealAreaEnd OnSpawnEtherealAreaEnd;

    public GameObject etherealGelAreaPrefab;

    public GameObject tilemapGameObject;
    Tilemap tilemap;

    List<GameObject[]> etherealTeleporters;

    private void Awake() {
        etherealTeleporters = new List<GameObject[]>();
    }

    private void Start() {
        tilemapGameObject = GameObject.FindGameObjectWithTag("Floor");
        if (tilemapGameObject != null) {
            tilemap = tilemapGameObject.GetComponent<Tilemap>();
        }
    }

    private void OnEnable() {
        EtherealGel.OnTileCollision += HandleEtherealGelCollision;
        Ethereal.OnTeleport += TeleportObjectTo;
    }

    private void OnDisable() {
        EtherealGel.OnTileCollision -= HandleEtherealGelCollision;
        Ethereal.OnTeleport -= TeleportObjectTo;
    }

    public bool IsValidCollision(GameObject objCollidedWith) {
        if (objCollidedWith == tilemapGameObject) {
            return true;
        }
        return false;
    }

    void HandleEtherealGelCollision(GameObject initialAreaRef, Collision2D collision) {
        ContactPoint2D contact = collision.GetContact(0);
        Vector2 relativeVelocity = collision.relativeVelocity;
        Dictionary<GameObject, Vector3> etherealAreaEntry = new Dictionary<GameObject, Vector3>();
        Vector3 tileWorldPosition = Vector3.zero;
        Quaternion newRotation = Quaternion.FromToRotation(Vector2.up, contact.normal);
        GameObject matchingAreaRef = SpawnMatchingEtherealArea(contact, newRotation, relativeVelocity);
        UpdateEtherealTilePositions(initialAreaRef, matchingAreaRef);
    }

    void UpdateEtherealTilePositions(GameObject first, GameObject second) {
        GameObject[] newPair = new GameObject[2];
        newPair[0] = first;
        newPair[1] = second;

        etherealTeleporters.Add(newPair);
    }

    GameObject SpawnMatchingEtherealArea(ContactPoint2D contact, Quaternion rotation, Vector2 velocity) {
        Vector3 tileWorldPosition = Vector3.zero;
        Vector3Int startPos = Vector3Int.zero;
        Vector3Int endPos = Vector3Int.zero;

        tileWorldPosition.x = contact.point.x - 0.01f * contact.normal.x;
        tileWorldPosition.y = contact.point.y - 0.01f * contact.normal.y;
        startPos = tilemap.WorldToCell(tileWorldPosition);
        endPos = startPos;


        if (Mathf.Abs(rotation.w) == 1) {
            // Place below
            for (; ; ) {
                endPos.y -= 1;
                if (tilemap.GetTile(endPos) == null) {
                    Vector3 newRot = rotation.eulerAngles;
                    newRot.z += 180;
                    return Instantiate(etherealGelAreaPrefab, new Vector3(contact.point.x, endPos.y + 1), Quaternion.Euler(newRot));
                }
            }
        } else if (Mathf.Abs(rotation.x) == 1) {
            // Place Above
            for (; ; ) {
                endPos.y += 1;
                if (tilemap.GetTile(endPos) == null) {
                    Vector3 newRot = rotation.eulerAngles;
                    newRot.z += 180;
                    return Instantiate(etherealGelAreaPrefab, new Vector3(contact.point.x, endPos.y), Quaternion.Euler(newRot));
                }
            }
        } else {
            if (velocity.x < 0) {
                // Place Right
                for (; ; ) {
                    endPos.x += 1;
                    if (tilemap.GetTile(endPos) == null) {
                        return Instantiate(etherealGelAreaPrefab, new Vector3(endPos.x, contact.point.y), Quaternion.Inverse(rotation));
                    }
                }
            } else {
                // Place Left
                for (; ; ) {
                    endPos.x -= 1;
                    if (tilemap.GetTile(endPos) == null) {
                        return Instantiate(etherealGelAreaPrefab, new Vector3(endPos.x + 1, contact.point.y), Quaternion.Inverse(rotation));
                    }
                }
            }
        }
    }

    void TeleportObjectTo(GameObject gameObj, GameObject destination) {
        foreach (GameObject[] teleporterPair in etherealTeleporters) {
            bool isValidDestination = teleporterPair.Any(teleporter => teleporter == destination);
            if (isValidDestination) {
                foreach (GameObject teleporter in teleporterPair) {
                    if (teleporter != destination) {
                        gameObj.transform.position = teleporter.GetComponent<TeleportPosition>().GetPosition();
                    }
                }
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour {
    public delegate GameObject SpawnEtherealAreaEnd(float x, float y, Vector3 normal);
    public static event SpawnEtherealAreaEnd OnSpawnEtherealAreaEnd;

    public GameObject etherealGelAreaPrefab;

    public GameObject tilemapGameObject;
    Tilemap tilemap;

    List<Dictionary<GameObject, Vector3>> etherealTilePositions;

    private void Awake() {
        etherealTilePositions = new List<Dictionary<GameObject, Vector3>>();
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

        etherealAreaEntry.Add(initialAreaRef, new Vector3(initialAreaRef.transform.position.x - 0.5f, initialAreaRef.transform.position.y, initialAreaRef.transform.position.z));
        etherealAreaEntry.Add(matchingAreaRef, new Vector3(matchingAreaRef.transform.position.x + 0.5f, matchingAreaRef.transform.position.y, matchingAreaRef.transform.position.z));
        etherealTilePositions.Add(etherealAreaEntry);
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
            Debug.Log("Place below");
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
        foreach (Dictionary<GameObject, Vector3> etherealAreaEntry in etherealTilePositions) {
            if (etherealAreaEntry.ContainsKey(destination)) {
                if (etherealAreaEntry.Count != 2) {
                    Debug.LogWarning("Ethereal Area Entry does not have length 2.");
                    return;
                }
                foreach (KeyValuePair<GameObject, Vector3> obj in etherealAreaEntry) {
                    if (obj.Key != destination) {
                        gameObj.transform.position = obj.Value;
                    }
                }
            }
        }
    }
}

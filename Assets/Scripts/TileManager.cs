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
        Dictionary<GameObject, Vector3> etherealAreaEntry = new Dictionary<GameObject, Vector3>();
        Vector3 tileWorldPosition = Vector3.zero;
        GameObject matchingAreaRef = SpawnMatchingEtherealArea(contact);


        etherealAreaEntry.Add(initialAreaRef, new Vector3(initialAreaRef.transform.position.x - 0.5f, initialAreaRef.transform.position.y, initialAreaRef.transform.position.z));
        etherealAreaEntry.Add(matchingAreaRef, new Vector3(matchingAreaRef.transform.position.x + 0.5f, matchingAreaRef.transform.position.y, matchingAreaRef.transform.position.z));
        etherealTilePositions.Add(etherealAreaEntry);
    }

    GameObject SpawnMatchingEtherealArea(ContactPoint2D contact) {
        Vector3 tileWorldPosition = Vector3.zero;
        Vector3Int startPos = Vector3Int.zero;
        Vector3Int endPos = Vector3Int.zero;

        tileWorldPosition.x = contact.point.x - 0.01f * contact.normal.x;
        tileWorldPosition.y = contact.point.y - 0.01f * contact.normal.y;
        startPos = tilemap.WorldToCell(tileWorldPosition);
        endPos = startPos;

        for (; ; ) {
            endPos.x += 1;
            if (tilemap.GetTile(endPos) == null) {
                Quaternion newRot = Quaternion.FromToRotation(Vector2.up, contact.normal);
                return Instantiate(etherealGelAreaPrefab, new Vector3(endPos.x, contact.point.y), newRot);
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

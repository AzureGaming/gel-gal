using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Gel : MonoBehaviour {
    public GameObject collisionParticlesPrefab;
    public GameObject gelAreaPrefab;

    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.LogWarning("Method not implemented.");
    }

    protected void SpawnArea(float xPos, float yPos, Vector3 normal) {
        Vector3 collisionPos = new Vector3(xPos, yPos);
        Quaternion newRot = Quaternion.FromToRotation(Vector2.up, normal);
        Vector3 hitPos = Vector3.zero;
        hitPos.x = xPos - 0.01f * normal.x;
        hitPos.y = yPos - 0.01f * normal.y;

        GameObject instance = Instantiate(gelAreaPrefab, hitPos, newRot);
        Instantiate(collisionParticlesPrefab, transform.position, Quaternion.identity);
        //OnSpawnGelArea?.Invoke(instance, 0);
        Destroy(gameObject);
    }
}

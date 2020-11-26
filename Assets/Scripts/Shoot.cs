using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    public GameObject bounceGelPrefab;
    public GameObject etherealGelPrefab;
    public GameObject cratePrefab;
    public GameObject projectileOrigin;
    public GameObject crateOrigin;

    Vector2 lastDirection;

    private void OnEnable() {
        Player.OnShoot += SpawnProjectile;
    }

    private void OnDisable() {
        Player.OnShoot -= SpawnProjectile;
    }

    void SpawnProjectile(Vector2 direction, bool hasCrate, GameManager.GelType gelType) {
        lastDirection = direction;
        if (hasCrate) {
            SpawnCrateProjectile();
        } else if (gelType == GameManager.GelType.Bounce) {
            SpawnGelProjectile(bounceGelPrefab);
        } else if (gelType == GameManager.GelType.Ethereal) {
            SpawnGelProjectile(etherealGelPrefab);
        }
    }

    void SpawnGelProjectile(GameObject gelPrefab) {
        GameObject arrowRef = Instantiate(gelPrefab, transform.position, transform.rotation);
        Vector3 velocity = projectileOrigin.transform.right * (lastDirection.magnitude * 2f);

        arrowRef.GetComponent<Rigidbody2D>().velocity = velocity;
    }

    void SpawnCrateProjectile() {
        Vector3 pos = crateOrigin.transform.position;
        GameObject crateRef = Instantiate(cratePrefab, pos, transform.rotation);
        Vector3 velocity = projectileOrigin.transform.right * (lastDirection.magnitude * 2f);

        crateRef.GetComponent<Rigidbody2D>().velocity = velocity;
    }
}

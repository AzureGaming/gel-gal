using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour {
    public GameObject gelPrefab;
    public GameObject cratePrefab;
    public GameObject projectileOrigin;

    [SerializeField] float launchForce = 2f;

    private void OnEnable() {
        Player.OnShoot += SpawnProjectile;
    }

    private void OnDisable() {
        Player.OnShoot -= SpawnProjectile;
    }

    void SpawnProjectile(Vector2 direction, bool hasCrate) {
        if (hasCrate) {
            GameObject crateRef = Instantiate(cratePrefab, transform.position, transform.rotation);
            Vector3 velocity = projectileOrigin.transform.right * (direction.magnitude * 2f);

            crateRef.GetComponent<Rigidbody2D>().velocity = velocity;

        } else {
            GameObject arrowRef = Instantiate(gelPrefab, transform.position, transform.rotation);
            Vector3 velocity = projectileOrigin.transform.right * (direction.magnitude * 2f);

            arrowRef.GetComponent<Rigidbody2D>().velocity = velocity;
        }
    }
}

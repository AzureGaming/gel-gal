using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelTrajectory : MonoBehaviour {
    public GameObject gelPrefab;
    public GameObject pointPrefab;
    public GameObject[] points;
    public int numberOfPoints;

    [SerializeField] float launchFactor = 2f;
    [SerializeField] float launchForce = 9f;

    Rigidbody2D rb;

    Vector2 direction;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start() {
        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++) {
            points[i] = Instantiate(pointPrefab, transform.position, Quaternion.identity);
        }
    }

    private void Update() {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerPos = transform.position;
        direction = mousePos - playerPos;
        transform.right = direction;


        launchForce = direction.magnitude * launchFactor;

        for (int i = 0; i < points.Length; i++) {
            points[i].transform.position = PointPosition(i * 0.1f);
        }
    }

    private void OnEnable() {
        Player.OnShoot += Shoot;
    }

    private void OnDisable() {
        Player.OnShoot -= Shoot;
    }

    void Shoot() {
        GameObject arrowRef = Instantiate(gelPrefab, transform.position, transform.rotation);
        Vector3 velocity = transform.right * launchForce;

        arrowRef.GetComponent<Rigidbody2D>().velocity = velocity;
    }

    Vector2 PointPosition(float time) {
        Vector2 currentPosition = (Vector2)transform.position + (direction.normalized * launchForce * time) + 0.5f * Physics2D.gravity * (time * time);
        return currentPosition;
    }
}

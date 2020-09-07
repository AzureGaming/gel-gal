using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelTrajectory : MonoBehaviour {
    public GameObject player;
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
        Vector2 playerPos = player.transform.position;
        direction = mousePos - playerPos;
        player.transform.right = direction;

        launchForce = direction.magnitude * launchFactor;

        for (int i = 0; i < points.Length; i++) {
            points[i].transform.position = PointPosition(i * 0.1f);
        }
    }

    private void OnEnable() {
        Player.OnAimGel += Shoot;
    }

    private void OnDisable() {
        Player.OnAimGel -= Shoot;
    }

    void Shoot() {
        GameObject arrowRef = Instantiate(gelPrefab, transform.position, transform.rotation);
        arrowRef.GetComponent<Rigidbody2D>().velocity = transform.right * launchForce;
    }

    Vector2 PointPosition(float time) {
        Vector2 currentPosition = (Vector2)transform.position + (direction.normalized * launchForce * time) + 0.5f * Physics2D.gravity * (time * time);
        return currentPosition;
    }
}

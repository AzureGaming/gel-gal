using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelTrajectory : MonoBehaviour {
    public GameObject gelPrefab;
    public GameObject pointPrefab;
    public GameObject[] points;
    public GameObject prefabContainer;
    public int numberOfPoints;

    [SerializeField] float launchFactor = 2f;

    Rigidbody2D rb;

    Vector2 trajectoryDirection;
    float launchForce;
    bool shouldRender = false;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() {
        Player.OnAim += SetupRender;
    }

    private void OnDisable() {
        Player.OnAim -= SetupRender;
    }

    private void Start() {
        points = new GameObject[numberOfPoints];
        for (int i = 0; i < numberOfPoints; i++) {
            points[i] = Instantiate(pointPrefab, prefabContainer.transform);
        }
    }

    private void Update() {
        if (shouldRender) {
            RenderTrajectory();
        }
    }

    void RenderTrajectory() {
        launchForce = trajectoryDirection.magnitude * launchFactor;
        for (int i = 0; i < points.Length; i++) {
            points[i].transform.position = PointPosition(i * 0.1f);
        }
    }

    void SetupRender(Vector2? direction) {
        if (direction == null) {
            prefabContainer.SetActive(false);
            shouldRender = false;
        } else {
            prefabContainer.SetActive(true);
            shouldRender = true;
            trajectoryDirection = (Vector2)direction;
        }
    }

    Vector2 PointPosition(float time) {
        Vector2 currentPosition = (Vector2)transform.position + (trajectoryDirection.normalized * launchForce * time) + 0.5f * Physics2D.gravity * (time * time);
        return currentPosition;
    }
}

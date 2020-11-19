using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLayer : MonoBehaviour {
    public GameObject[] clouds;
    public GameObject bounds;

    private void Start() {
        InitSpawn();
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.E)) {
            InitSpawn();
        }
    }

    void InitSpawn() {
        StartCoroutine(SpawnCloud());
    }

    IEnumerator SpawnCloud() {
        for (; ; ) {
            yield return new WaitForSeconds(1f);
            int randomIndex = Random.Range(0, clouds.Length);
            GameObject randomCloud = clouds[randomIndex];
            float maxX = bounds.transform.position.x + bounds.transform.localScale.x / 2;
            float maxY = bounds.transform.position.y + bounds.transform.localScale.y / 2;
            float minX = -maxX;
            float minY = bounds.transform.position.y - bounds.transform.localScale.y / 2;
            Vector3 pos = new Vector3(maxX, minY);

            Instantiate(randomCloud, pos, Quaternion.identity, transform);
        }
    }
}

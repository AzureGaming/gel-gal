using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudLayer : MonoBehaviour {
    public GameObject[] clouds;
    public CloudSpawnArea spawnArea;

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
            float maxX = spawnArea.transform.position.x + spawnArea.transform.localScale.x / 2;
            float maxY = spawnArea.transform.position.y + spawnArea.transform.localScale.y / 2;
            float minX = -maxX;
            float minY = spawnArea.transform.position.y - spawnArea.transform.localScale.y / 2;
            Vector3 randomPos = new Vector3(maxX, minY);

            Instantiate(randomCloud, randomPos, Quaternion.identity, transform);
        }
    }
}

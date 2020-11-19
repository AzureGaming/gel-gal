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
            float randomPosY = Random.Range(spawnArea.minY, spawnArea.maxY);
            Vector3 randomPos = new Vector3(spawnArea.maxX, randomPosY);

            GameObject cloud = Instantiate(randomCloud, transform, false);
            cloud.transform.position = randomPos;
        }
    }
}

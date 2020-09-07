using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDoneParticles : MonoBehaviour {
    ParticleSystem ps;

    private void Awake() {
        ps = GetComponent<ParticleSystem>();
    }

    void Update() {
        if (!ps.IsAlive()) {
            Destroy(gameObject);
        }
    }
}

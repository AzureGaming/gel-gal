using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelArea : MonoBehaviour {
    private void OnEnable() {
        GelLimit.OnDespawnGel += Despawn;
    }

    private void OnDisable() {
        GelLimit.OnDespawnGel -= Despawn;
    }

    void Despawn(GameObject objRef) {
        if (gameObject == objRef) {
            OnDespawn();
            Destroy(gameObject);
        }
    }

    protected virtual void OnDespawn() {
        // ...
    }
}

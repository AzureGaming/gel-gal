using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GelArea : MonoBehaviour {
    public delegate void SpawnGelArea(GameObject gelAreaRef, GameManager.GelType type);
    public static event SpawnGelArea OnSpawnGelArea;

    protected GameManager.GelType type;

    protected virtual void OnEnable() {
        GelLimit.OnDespawnGel += Despawn;
    }

    protected virtual void OnDisable() {
        GelLimit.OnDespawnGel -= Despawn;
    }

    protected virtual void Start() {
        OnSpawnGelArea?.Invoke(gameObject, GetGelType());
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

    protected virtual GameManager.GelType GetGelType() {
        return type;
    }
}

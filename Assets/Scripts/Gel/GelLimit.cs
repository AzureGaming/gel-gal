using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GelLimit : Singleton {
    public delegate void DespawnGel(GameObject objRef);
    public static event DespawnGel OnDespawnGel;

    Dictionary<int, int> LIMITS;
    Dictionary<int, List<GameObject>> limitTracker;

    static GelLimit instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(this);
        }

        LIMITS = new Dictionary<int, int>();
        LIMITS.Add(0, 1);

        limitTracker = new Dictionary<int, List<GameObject>>();
    }

    private void OnEnable() {
        Gel.OnSpawnGelArea += CheckLimit;
        LevelManager.OnLoadLevel += Reset;
    }

    private void OnDisable() {
        Gel.OnSpawnGelArea -= CheckLimit;
        LevelManager.OnLoadLevel -= Reset;
    }

    void CheckLimit(GameObject gelAreaRef, int type) {
        if (!limitTracker.ContainsKey(type)) {
            limitTracker.Add(type, new List<GameObject>());
        }

        limitTracker[type].Add(gelAreaRef);

        if (limitTracker[type].Count > LIMITS[type]) {
            GameObject refToDelete = limitTracker[type].First();
            limitTracker[type].Remove(refToDelete);
            //Destroy(refToDelete);
            OnDespawnGel?.Invoke(refToDelete);
        }
    }

    void Reset() {
        limitTracker = new Dictionary<int, List<GameObject>>();
    }
}

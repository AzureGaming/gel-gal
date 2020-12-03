using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GelLimit : Singleton {
    public delegate void DespawnGel(GameObject objRef);
    public static event DespawnGel OnDespawnGel;

    Dictionary<GameManager.GelType, int> LIMITS;
    Dictionary<GameManager.GelType, List<GameObject>> limitTracker;

    protected override void Awake() {
        base.Awake();
        SetLimits();
        ResetLimitTracker();
    }

    private void OnEnable() {
        GelArea.OnSpawnGelArea += CheckLimit;
        LevelManager.OnLoadLevel += ResetLimitTracker;
    }

    private void OnDisable() {
        GelArea.OnSpawnGelArea -= CheckLimit;
        LevelManager.OnLoadLevel -= ResetLimitTracker;
    }

    void CheckLimit(GameObject gelAreaRef, GameManager.GelType type) {
        if (!limitTracker.ContainsKey(type)) {
            limitTracker[type] = new List<GameObject>();
        }

        limitTracker[type].Add(gelAreaRef);

        if (limitTracker[type].Count > LIMITS[type]) {
            GameObject refToDelete = limitTracker[type].First();
            limitTracker[type].Remove(refToDelete);
            OnDespawnGel?.Invoke(refToDelete);
        }
    }

    void ResetLimitTracker() {
        limitTracker = new Dictionary<GameManager.GelType, List<GameObject>>();
    }

    void SetLimits() {
        LIMITS = new Dictionary<GameManager.GelType, int>();
        LIMITS.Add(GameManager.GelType.Bounce, 1);
        LIMITS.Add(GameManager.GelType.Ethereal, 2);
    }
}

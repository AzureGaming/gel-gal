using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherealArea : GelArea {
    public delegate void Remove();
    public static Remove OnRemove;

    private void Awake() {
        type = GameManager.GelType.Ethereal;
    }

    protected override void OnDespawn() {
        OnRemove?.Invoke();
    }
}

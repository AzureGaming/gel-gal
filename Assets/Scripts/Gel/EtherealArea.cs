﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherealArea : GelArea {
    public delegate void Remove();
    public static Remove OnRemove;

    TileManager tileManager;

    private void Awake() {
        tileManager = FindObjectOfType<TileManager>();
        type = GameManager.GelType.Ethereal;
    }

    protected override void OnDespawn() {
        OnRemove?.Invoke();
    }
}

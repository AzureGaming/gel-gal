using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherealArea : GelArea {
    TileManager tileManager;

    private void Awake() {
        tileManager = FindObjectOfType<TileManager>();
    }

    protected override void OnDespawn() {
        Vector3Int pos = tileManager.GetEtherealTilePos();
    }
}

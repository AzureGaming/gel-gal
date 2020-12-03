using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherealArea : GelArea {
    public delegate void Remove();
    public static Remove OnRemove;

    public BoxCollider2D collider2d;
    public GameObject teleportPositionObj;

    bool exitTriggered;
    bool canTeleport = true;

    private void Awake() {
        type = GameManager.GelType.Ethereal;
    }

    protected override void OnDespawn() {
        OnRemove?.Invoke();
    }

    public void DetectExit() {
        canTeleport = false;
        exitTriggered = false;
        StartCoroutine(EnableTeleportation());
    }

    public void Teleport(GameObject objToTeleport) {
        if (canTeleport) {
            objToTeleport.transform.position = teleportPositionObj.transform.position;
        }
    }

    IEnumerator EnableTeleportation() {
        yield return new WaitUntil(() =>  exitTriggered);
        canTeleport = true;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        exitTriggered = true;
    }
}

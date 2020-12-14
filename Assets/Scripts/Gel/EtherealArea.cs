using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherealArea : GelArea {
    public delegate void Remove();
    public static Remove OnRemove;

    public delegate void TeleportStart(GameObject objToTeleport, GameObject destination);
    public static TeleportStart OnTeleportStart;

    public delegate void TeleportEnd(GameObject obj, Action cb);
    public static TeleportEnd OnTeleportEnd;

    public BoxCollider2D collider2d;

    bool exitTriggered;
    bool canTeleport = true;

    private void Awake() {
        type = GameManager.GelType.Ethereal;
    }

    protected override void OnEnable() {
        base.OnEnable();
        PlayerMovementController.OnTeleport += UpdatePosition;
    }

    protected override void OnDisable() {
        base.OnDisable();
        PlayerMovementController.OnTeleport -= UpdatePosition;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        //exitTriggered = true;
    }

    protected override void OnDespawn() {
        OnRemove?.Invoke();
    }

    public void DetectExit() {
        canTeleport = false;
        exitTriggered = false;
        StartCoroutine(EnableTeleportation());
    }

    public void StartTeleport(GameObject objToTeleport, GameObject destination) {
        if (canTeleport) {
            OnTeleportStart?.Invoke(objToTeleport, destination);
        }
    }

    void UpdatePosition(GameObject obj, GameObject destination) {
        StartCoroutine(UpdatePositionRoutine(obj, destination));
    }

    IEnumerator UpdatePositionRoutine(GameObject objToTeleport, GameObject destination) {
        float waitTime = 1f;
        float elapsedTime = 0f;
        Vector3 startPos = objToTeleport.transform.position;
        Vector3 endPos = destination.transform.position;

        while (elapsedTime < waitTime) {
            Vector3 newPos = Vector3.Lerp(startPos, endPos, (elapsedTime / waitTime));
            objToTeleport.transform.position = newPos;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objToTeleport.transform.position = endPos;
        OnTeleportEnd?.Invoke(objToTeleport, () => ResetRigidBody());
    }

    IEnumerator EnableTeleportation() {
        yield return new WaitUntil(() => exitTriggered);
        canTeleport = true;
    }

    void ResetRigidBody() {
    }
}

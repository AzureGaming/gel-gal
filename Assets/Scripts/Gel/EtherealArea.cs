using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherealArea : GelArea {
    public delegate void Remove();
    public static Remove OnRemove;

    public delegate void Teleport(GameObject obj);
    public static Teleport OnTeleport;

    public delegate void TeleportEnd(GameObject obj, Action cb);
    public static TeleportEnd OnTeleportEnd;

    public BoxCollider2D collider2d;
    public GameObject teleportPositionObj;

    bool exitTriggered;
    bool canTeleport = true;

    private void Awake() {
        type = GameManager.GelType.Ethereal;
    }

    private void OnTriggerExit2D(Collider2D collision) {
        exitTriggered = true;
    }

    protected override void OnDespawn() {
        OnRemove?.Invoke();
    }

    public void DetectExit() {
        canTeleport = false;
        exitTriggered = false;
        StartCoroutine(EnableTeleportation());
    }

    public void UpdatePosition(GameObject objToTeleport) {
        if (canTeleport) {
            OnTeleport?.Invoke(objToTeleport);
            StartCoroutine(UpdatePositionRoutine(objToTeleport));
        }
    }

    IEnumerator UpdatePositionRoutine(GameObject objToTeleport) {
        float time = 0;
        objToTeleport.GetComponent<Rigidbody2D>().isKinematic = true;
        objToTeleport.GetComponent<Rigidbody2D>().simulated = false;
        for (; ; ) {
            if (objToTeleport.transform.position == teleportPositionObj.transform.position) {
                OnTeleportEnd?.Invoke(objToTeleport, () => ResetRigidBody(objToTeleport.GetComponent<Rigidbody2D>()));
                yield break;
            }
            Vector3 newPos = Vector3.Lerp(objToTeleport.transform.position, teleportPositionObj.transform.position, time * Time.deltaTime);
            objToTeleport.transform.position = newPos;
            time += 0.01f;
            yield return null;
        }
    }

    IEnumerator EnableTeleportation() {
        yield return new WaitUntil(() => exitTriggered);
        canTeleport = true;
    }

    void ResetRigidBody(Rigidbody2D rb) {
        rb.isKinematic = false;
        rb.simulated = true;
    }
}

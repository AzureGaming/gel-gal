using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    public Animator top;
    public Animator bottom;

    BoxCollider2D collider2d;

    private void Awake() {
        collider2d = GetComponent<BoxCollider2D>();    
    }

    private void OnEnable() {
        RoomManager.OnRoomCleared += HandleRoomClear;
    }

    void HandleRoomClear(bool valid) {
        if (valid) {
            Open();
        } else {
            Close();
        }
    }

    void Open() {
        top.SetTrigger("Open");
        bottom.SetTrigger("Open");
        StartCoroutine(DisableCollider());
    }

    void Close() {
        top.SetTrigger("Close");
        bottom.SetTrigger("Close");
        StartCoroutine(EnableCollider());
    }

    IEnumerator DisableCollider() {
        //yield return new WaitForSeconds(0.5f);
        collider2d.enabled = false;
        yield break;
    }

    IEnumerator EnableCollider() {
        //yield return new WaitForSeconds(0.5f);
        collider2d.enabled = true;
        yield break;
    }
}

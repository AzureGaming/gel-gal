using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour {
    public delegate void RoomClear();
    public static event RoomClear OnRoomClear;

    public Animator top;
    public Animator bottom;

    BoxCollider2D collider2d;

    float openTime = 0f;
    float closeTime = 0f;
    // No animation has played when first instantiated so 
    // we must skip clamping to 1.
    bool firstTime = true;

    private void Awake() {
        collider2d = GetComponent<BoxCollider2D>();
    }

    private void OnEnable() {
        RoomManager.OnRoomCleared += HandleRoomClear;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GameManager.GOAL)) {
            OnRoomClear?.Invoke();
        }
    }

    void HandleRoomClear(bool valid) {
        if (valid) {
            Open();
        } else {
            Close();
        }
    }

    void Open() {
        top.Play("Open", 0, firstTime ? 0 : GetTimeInRange(top));
        bottom.Play("Open", 0, firstTime ? 0 : GetTimeInRange(bottom));
        StartCoroutine(DisableCollider());
        firstTime = false;
    }

    void Close() {
        top.Play("Close", 0, firstTime ? 0 : GetTimeInRange(top));
        bottom.Play("Close", 0, firstTime ? 0 : GetTimeInRange(bottom));
        StartCoroutine(EnableCollider());
        firstTime = false;
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

    float GetTimeInRange(Animator targetAnim) {
        // Required to clamp as clip is wrapping
        return 1 - Mathf.Clamp(GetCurrentAnimatorTime(targetAnim), 0, 1); 
    }

    float GetCurrentAnimatorTime(Animator targetAnim, int layer = 0) {
        AnimatorStateInfo animState = targetAnim.GetCurrentAnimatorStateInfo(layer);
        float currentTime = animState.normalizedTime;
        return currentTime;
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelWheel : MonoBehaviour {
    public delegate void Rotate(float angle);
    public static event Rotate OnRotate;
    public delegate void DoneRotate();
    public static event DoneRotate OnDoneRotate;

    RectTransform rectTransform;

    private void OnEnable() {
        Player.OnEquipNextGel += Next;
    }

    private void OnDisable() {
        Player.OnEquipNextGel -= Next;
    }

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    void Next() {
        StartCoroutine(RotateWheel());
    }

    IEnumerator RotateWheel() {
        float waitTime = 1f;
        float elapsedTime = 0f;
        Vector3 rotation = rectTransform.rotation.eulerAngles;
        while (elapsedTime < waitTime) {
            float angle = Mathf.LerpAngle(rotation.z, rotation.z - 120, (elapsedTime / waitTime));
            Vector3 newRot = rotation;
            newRot.z = angle;
            elapsedTime += Time.deltaTime;

            rectTransform.eulerAngles = newRot;
            OnRotate?.Invoke(angle);
            yield return null;
        }
        Vector3 finalRot = rotation;
        finalRot.z -= 120f;
        rectTransform.eulerAngles = finalRot;
        OnDoneRotate?.Invoke();
    }
}

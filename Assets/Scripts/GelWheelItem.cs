using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GelWheelItem : MonoBehaviour {
    public RectTransform parentTransform;

    RectTransform rectTransform;

    void OnEnable() {
        GelWheel.OnRotate += Rotate;
    }

    void OnDisable() {
        GelWheel.OnRotate -= Rotate;
    }

    void Awake() {
        rectTransform = GetComponent<RectTransform>();
    }

    void Start() {
        rectTransform.localEulerAngles = -parentTransform.eulerAngles;
    }

    void Rotate(float angle) {
        Vector3 newRot = rectTransform.localEulerAngles;
        newRot.z = -angle;
        rectTransform.localEulerAngles = newRot;
    }
}

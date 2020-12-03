using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPosition : MonoBehaviour {
    public GameObject obj;
    public Vector3 GetPosition() {
        return obj.transform.position;
    }
}

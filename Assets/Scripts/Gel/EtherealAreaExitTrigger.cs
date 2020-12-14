using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EtherealAreaExitTrigger : MonoBehaviour {
    public delegate void Exit(Collider2D collision, GameObject self);
    public static Exit OnExit;

    private void OnTriggerExit2D(Collider2D collision) {
        OnExit?.Invoke(collision, gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour {
    public delegate void ButtonActivated(bool activated);
    public static event ButtonActivated OnButtonActivate;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GameManager.BUTTON_TRIGGER)) {
            OnButtonActivate?.Invoke(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag(GameManager.BUTTON_TRIGGER)) {
            OnButtonActivate?.Invoke(false);
        }
    }
}

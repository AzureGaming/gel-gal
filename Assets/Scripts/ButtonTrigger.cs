using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTrigger : MonoBehaviour {
    public delegate void ButtonActivated(bool activated);
    public static event ButtonActivated OnButtonActivate;

    public Button button;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GameManager.BUTTON_TRIGGER)) {
            Debug.Log("Hello" + collision.name);
            //OnButtonActivate?.Invoke(true);
            //button.ButtonDown();
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag(GameManager.BUTTON_TRIGGER)) {
            OnButtonActivate?.Invoke(false);
            button.ButtonUp();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressureButton : Button {
    private void OnTriggerExit2D(Collider2D collision) {
        if (isActivated) {
            base.OnButtonActivated(false);
            isActivated = false;
            ButtonUp();
        }
    }
}

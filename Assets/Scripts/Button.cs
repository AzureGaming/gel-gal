using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour {
    public Sprite upSprite;
    public Sprite downSprite;

    public SpriteRenderer spriteR;

    public void ButtonUp() {
        spriteR.sprite = upSprite;
    }

    public void ButtonDown() {
        spriteR.sprite = downSprite;
    }
}

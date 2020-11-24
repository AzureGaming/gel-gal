using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class City : MonoBehaviour {
    public Sprite[] foregrounds;
    public Sprite[] backgrounds;
    public Sprite[] middlegrounds;
    public SpriteRenderer foreground;

    private void Start() {
        StartCoroutine(UpdateForeground());
    }

    IEnumerator UpdateForeground() {
        int index = 0;
        for (; ; ) {
            foreground.sprite = foregrounds[index];
            if (index > foregrounds.Length- 1) {
                index = 0;
            } else {
                index++;
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomSpriteStart : MonoBehaviour {
    public Sprite[] sprites;

    SpriteRenderer spriteR;

    private void Awake() {
        spriteR = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        int randomIndex = Random.Range(0, sprites.Length);
        spriteR.sprite = sprites[randomIndex];
    }
}

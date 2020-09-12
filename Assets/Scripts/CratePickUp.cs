using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CratePickUp : MonoBehaviour {
    public delegate void PickUp();
    public static event PickUp OnPickUp;

    public GameObject textPrompt;

    bool canPickUp = false;

    private void Start() {
        textPrompt.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.F) && canPickUp) {
            OnPickUp?.Invoke();
            Destroy(transform.parent.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag(GameManager.PLAYER_TAG)) {
            canPickUp = true;
            textPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.CompareTag(GameManager.PLAYER_TAG)) {
            canPickUp = false;
            textPrompt.SetActive(false);
        }
    }
}

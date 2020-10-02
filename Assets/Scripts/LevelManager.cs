using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    private void OnEnable() {
        Goal.OnRoomClear += LoadNextLevel;
        Player.OnDeath += Lose;
    }

    private void OnDisable() {
        Goal.OnRoomClear -= LoadNextLevel;
        Player.OnDeath -= Lose;
    }

    void LoadNextLevel() {
        Debug.Log("Load next level");
    }

    void Lose() {
        Debug.Log("Lose");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {
    public int levelIndex = 0;
    static LevelManager instance;

    private void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(this);
        } else {
            Destroy(this);
        }
    }

    private void OnEnable() {
        GoalTrigger.OnRoomClear += LoadNextLevel;
        Player.OnDeath += Lose;
    }

    private void OnDisable() {
        GoalTrigger.OnRoomClear -= LoadNextLevel;
        Player.OnDeath -= Lose;
    }

    void LoadNextLevel() {
        Debug.Log("Load " + levelIndex);
        levelIndex++;
        SceneManager.LoadScene(levelIndex);
    }

    void Lose() {
        Debug.Log("Lose");
    }
}

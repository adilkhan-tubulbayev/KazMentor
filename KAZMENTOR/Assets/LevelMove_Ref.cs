using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMove_Ref : MonoBehaviour {
    public int sceneBuildIndex;
    private bool playerInTrigger = false;

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInTrigger = true;
            print("Player entered trigger");
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInTrigger = false;
            print("Player exited trigger");
        }
    }

    private void Update() {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.E)) {
            print("Switching Scene to " + sceneBuildIndex);
            SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
        }
    }
}

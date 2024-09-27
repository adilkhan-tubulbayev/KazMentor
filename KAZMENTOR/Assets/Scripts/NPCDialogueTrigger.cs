using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour {
    private GameObject dialogueCanvas; // Ссылка на Canvas с диалогом
    private Dialogue dialogueScript; // Добавлено для доступа к скрипту Dialogue
    private bool playerInRange;

    void Start() {
        dialogueCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
        if (dialogueCanvas != null) {
            dialogueCanvas.SetActive(false); // Изначально диалоговое окно отключено
            dialogueScript = dialogueCanvas.GetComponent<Dialogue>(); // Получение компонента Dialogue
            if (dialogueScript == null) {
                Debug.LogError("Dialogue script not found on DialogueCanvas!");
            }
        } else {
            Debug.LogError("Dialogue Canvas with tag 'DialogueCanvas' not found!");
        }
    }

    void Update() {
        if (playerInRange && Input.GetKeyDown(KeyCode.X)) {
            if (!dialogueCanvas.activeSelf) {
                AudioManager.Instance.PlayDialogueSound();
                dialogueCanvas.SetActive(true); // Активировать диалоговое окно
            }
            dialogueScript.ResetDialogue(); // Сброс и перезапуск диалога
        }
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            playerInRange = false;
            dialogueCanvas.SetActive(false); // Отключить диалоговое окно при выходе из зоны триггера
        }
    }
}
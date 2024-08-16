using UnityEngine;

public class NPCChatGPTTrigger : MonoBehaviour {
    public GameObject canvasAI; // Ссылка на CanvasAI
    public Player player; // Ссылка на объект игрока

    private bool playerInRange;

    void Start() {
        if (canvasAI != null) {
            canvasAI.SetActive(false); // Изначально диалоговое окно отключено
        } else {
            Debug.LogError("CanvasAI is not assigned in the inspector!");
        }

        if (player == null) {
            player = Player.Instance;
            if (player == null) {
                Debug.LogError("Player instance not found!");
            }
        }
    }

    void Update() {
        if (playerInRange && Input.GetKeyDown(KeyCode.C)) {
            OpenDialogue();
        }

        // Проверка нажатия клавиши выхода, например Escape
        if (canvasAI.activeInHierarchy && Input.GetKeyDown(KeyCode.Escape)) {
            ExitDialogue();
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
            canvasAI.SetActive(false); // Отключить диалоговое окно при выходе из зоны триггера
            player.isDialogueActive = false; // Разблокировать движение персонажа при выходе из зоны триггера
        }
    }

    public void OpenDialogue() {
        if (!canvasAI.activeInHierarchy) {
            canvasAI.SetActive(true); // Включить диалоговое окно
            player.isDialogueActive = true; // Заблокировать движение персонажа
        }
    }

    public void ExitDialogue() {
        AudioManager.Instance.PlayButtonSound();
        canvasAI.SetActive(false); // Выключить диалоговое окно
        player.isDialogueActive = false; // Разблокировать движение персонажа
    }
}

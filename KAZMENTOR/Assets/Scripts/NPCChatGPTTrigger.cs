using UnityEngine;

public class NPCChatGPTTrigger : MonoBehaviour {
    public GameObject canvasAI; // ������ �� CanvasAI
    public Player player; // ������ �� ������ ������

    private bool playerInRange;

    void Start() {
        if (canvasAI != null) {
            canvasAI.SetActive(false); // ���������� ���������� ���� ���������
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

        // �������� ������� ������� ������, �������� Escape
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
            canvasAI.SetActive(false); // ��������� ���������� ���� ��� ������ �� ���� ��������
            player.isDialogueActive = false; // �������������� �������� ��������� ��� ������ �� ���� ��������
        }
    }

    public void OpenDialogue() {
        if (!canvasAI.activeInHierarchy) {
            canvasAI.SetActive(true); // �������� ���������� ����
            player.isDialogueActive = true; // ������������� �������� ���������
        }
    }

    public void ExitDialogue() {
        AudioManager.Instance.PlayButtonSound();
        canvasAI.SetActive(false); // ��������� ���������� ����
        player.isDialogueActive = false; // �������������� �������� ���������
    }
}

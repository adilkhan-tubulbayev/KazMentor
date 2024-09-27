using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour {
    private GameObject dialogueCanvas; // ������ �� Canvas � ��������
    private Dialogue dialogueScript; // ��������� ��� ������� � ������� Dialogue
    private bool playerInRange;

    void Start() {
        dialogueCanvas = GameObject.FindGameObjectWithTag("DialogueCanvas");
        if (dialogueCanvas != null) {
            dialogueCanvas.SetActive(false); // ���������� ���������� ���� ���������
            dialogueScript = dialogueCanvas.GetComponent<Dialogue>(); // ��������� ���������� Dialogue
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
                dialogueCanvas.SetActive(true); // ������������ ���������� ����
            }
            dialogueScript.ResetDialogue(); // ����� � ���������� �������
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
            dialogueCanvas.SetActive(false); // ��������� ���������� ���� ��� ������ �� ���� ��������
        }
    }
}
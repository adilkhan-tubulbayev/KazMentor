using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour {
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed = 0.02f;
    public GameObject player; // ������ �� ������ ������
    public GameObject puzzle; // ������ �� ������ Puzzle
    public GameObject dialogueBox; // ������ �� ������ Dialogue Box
    public GameObject puzzleExitButton; // ������ �� ������ ������ �� ������
    public CircuitChecker circuitChecker; // ������ �� ������ CircuitChecker

    private int index;
    private Player playerScript; // ������ �� ������ ���������� ����������

    // Awake is called before Start
    void Awake() {
        if (puzzle != null) {
            puzzle.SetActive(false); // ������ Puzzle ��� ������
        } else {
            Debug.LogError("Puzzle is not assigned in the inspector!");
        }

        if (puzzleExitButton != null) {
            puzzleExitButton.SetActive(false); // ������ ������ ������ �� ������ ��� ������
        } else {
            Debug.LogError("Puzzle Exit Button is not assigned in the inspector!");
        }
    }

    // Start is called before the first frame update
    void Start() {
        textComponent.text = string.Empty;

        if (player != null) {
            playerScript = player.GetComponent<Player>();
        } else {
            Debug.LogError("Player is not assigned in the inspector!");
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (textComponent.text == lines[index]) {
                NextLine();
            } else {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue() {
        StopAllCoroutines(); // ���������� ����� ���������� ��������
        index = 0;
        StartCoroutine(TypeLine());
        if (playerScript != null) {
            playerScript.isDialogueActive = true; // ������������� �������� ���������
        }
    }

    IEnumerator TypeLine() {
        textComponent.text = string.Empty; // ������� ������ ����� ������� ����� �����

        // ���� ����� ��� ���������� UI
        yield return new WaitForEndOfFrame();

        string[] words = lines[index].Split(' ');

        foreach (string word in words) {
            foreach (char c in word.ToCharArray()) {
                textComponent.text += c;
                yield return new WaitForSeconds(textSpeed); // ����� ����� ���������
            }
            textComponent.text += ' '; // �������� ������ ����� �����
            AudioManager.Instance.PlayTalkSound(); // ��������������� ����� ��� ������� �����
            yield return new WaitForSeconds(textSpeed); // ��������� ����� ����� �����
        }
    }

    void NextLine() {
        if (index < lines.Length - 1) {
            index++;
            StartCoroutine(TypeLine());
        } else {
            gameObject.SetActive(false);
            if (playerScript != null) {
                playerScript.isDialogueActive = false; // �������������� �������� ���������
            }
        }
    }

    public void ResetDialogue() {
        StopAllCoroutines(); // ���������� ����� ���������� ��������
        index = 0; // ����� ������� �� ������
        StartDialogue(); // ���������� �������
    }

    public void ExitDialogue() {
        AudioManager.Instance.StopAudioClip(AudioManager.Instance.dialogue);
        AudioManager.Instance.PlayButtonSound();
        gameObject.SetActive(false); // ��������� ���������� ����
        if (playerScript != null) {
            playerScript.isDialogueActive = false; // �������������� �������� ���������
        }
    }

    public void ShowPuzzle() {
        AudioManager.Instance.PlayButtonSound();
        puzzle.SetActive(true); // �������� Puzzle
        puzzleExitButton.SetActive(true); // �������� ������ ������ �� ������
        dialogueBox.SetActive(false); // ������ ���������� ����
        AudioManager.Instance.StopAudioClip(AudioManager.Instance.dialogue);
        if (playerScript != null) {
            playerScript.isDialogueActive = true; // ������������� �������� ���������
        }
    }

    public void HidePuzzle() {
        AudioManager.Instance.PlayButtonSound();
        puzzle.SetActive(false); // ������ Puzzle
        puzzleExitButton.SetActive(false); // ������ ������ ������ �� ������
        if (playerScript != null) {
            playerScript.isDialogueActive = false; // �������������� �������� ���������
        }
    }

    public void GlobalExit() {
        HidePuzzle(); // ������ Puzzle
        if (circuitChecker != null) {
            circuitChecker.ExitResult(); // �������� ��� ��������������� �������
        }
    }
}

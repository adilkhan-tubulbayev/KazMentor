using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour {
    public InputField AnswerInputField; // TMP_InputField
    public GameObject ResultWindow;
    public TMP_Text ResultText; // TMP_Text ��� ��������� �������� TMP
    public Button PuzzleExitButton;
    public GameObject Player;

    private Player playerScript;

    private void Awake() {
        this.gameObject.SetActive(false); // ��������� ������ � ������ ����
        ResultWindow.SetActive(false); // ��������� ���� ���������� � ������ ����
        ResultText.text = ""; // ������� ������ ���������� � ������ ����
    }

    private void Start() {
        if (Player != null) {
            playerScript = Player.GetComponent<Player>();
        } else {
            Debug.LogError("Player is not assigned in the inspector!");
        }
    }

    public void ShowPuzzle() {
        this.gameObject.SetActive(true); // �������� ������
        if (playerScript != null) {
            playerScript.isDialogueActive = true; // ������������� �������� ���������
        }
    }

    public void CheckAnswer() {
        string answer = AnswerInputField.text;
        bool isCorrect = ValidateAnswer(answer);

        ResultWindow.SetActive(true);
        ResultText.text = isCorrect ? "�����!" : "�������!";
    }

    public void ExitPuzzle() {
        this.gameObject.SetActive(false); // ��������� ������
        ResultWindow.SetActive(false); // ��������� ���� ����������
        ResultText.text = ""; // ������� ������ ���������� ��� ������
        if (playerScript != null) {
            playerScript.isDialogueActive = false; // �������������� �������� ���������
        }
    }

    private bool ValidateAnswer(string answer) {
        // ������ ��� �������� ������
        return answer == "����������_�����"; // ������ ����������� ������
    }
}

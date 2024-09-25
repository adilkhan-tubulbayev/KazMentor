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
    public BarrierController barrierController; // ��������� ������ �� BarrierController

    private Player playerScript;
    private bool isPuzzleCorrect = false;

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
        AudioManager.Instance.PlayButtonSound();
        this.gameObject.SetActive(true); // �������� ������
        if (playerScript != null) {
            playerScript.isDialogueActive = true; // ������������� �������� ���������
        }
    }

    public void CheckAnswer() {
        AudioManager.Instance.PlayButtonSound();
        string answer = AnswerInputField.text;
        isPuzzleCorrect = ValidateAnswer(answer);

        ResultWindow.SetActive(true);
        ResultText.text = isPuzzleCorrect ? "�����!" : "�������!";

        if (barrierController != null) {
            barrierController.SetTextCorrect(isPuzzleCorrect);
        }
    }

    public void ExitPuzzle() {
        AudioManager.Instance.PlayButtonSound();
        if (isPuzzleCorrect) {
            barrierController.SetTextCorrect(true);
        }


        // ��������������� ����� ���������� �������������� ���� ��� ������ �� �����������
        AudioManager.Instance.PlayElectricFieldOffSound();


        this.gameObject.SetActive(false); // ��������� ������
        ResultWindow.SetActive(false); // ��������� ���� ����������
        ResultText.text = ""; // ������� ������ ���������� ��� ������
        if (playerScript != null) {
            playerScript.isDialogueActive = false; // �������������� �������� ���������
        }
    }

    private bool ValidateAnswer(string answer) {
        // ������ ��� �������� ������
        return answer == "14"; // ������ ����������� ������
    }

    public void ExitResult() {
        AudioManager.Instance.PlayButtonSound();
        ResultWindow.SetActive(false); // ��������� ���� ����������
    }
}

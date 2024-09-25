using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PuzzleManager : MonoBehaviour {
    public InputField AnswerInputField; // TMP_InputField
    public GameObject ResultWindow;
    public TMP_Text ResultText; // TMP_Text для текстовых объектов TMP
    public Button PuzzleExitButton;
    public GameObject Player;
    public BarrierController barrierController; // Добавляем ссылку на BarrierController

    private Player playerScript;
    private bool isPuzzleCorrect = false;

    private void Awake() {
        this.gameObject.SetActive(false); // Отключаем канвас в начале игры
        ResultWindow.SetActive(false); // Отключаем окно результата в начале игры
        ResultText.text = ""; // Очистка текста результата в начале игры
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
        this.gameObject.SetActive(true); // Включаем канвас
        if (playerScript != null) {
            playerScript.isDialogueActive = true; // Заблокировать движение персонажа
        }
    }

    public void CheckAnswer() {
        AudioManager.Instance.PlayButtonSound();
        string answer = AnswerInputField.text;
        isPuzzleCorrect = ValidateAnswer(answer);

        ResultWindow.SetActive(true);
        ResultText.text = isPuzzleCorrect ? "Верно!" : "Неверно!";

        if (barrierController != null) {
            barrierController.SetTextCorrect(isPuzzleCorrect);
        }
    }

    public void ExitPuzzle() {
        AudioManager.Instance.PlayButtonSound();
        if (isPuzzleCorrect) {
            barrierController.SetTextCorrect(true);
        }


        // Воспроизведение звука отключения электрического поля при выходе из головоломки
        AudioManager.Instance.PlayElectricFieldOffSound();


        this.gameObject.SetActive(false); // Отключаем канвас
        ResultWindow.SetActive(false); // Отключаем окно результата
        ResultText.text = ""; // Очистка текста результата при выходе
        if (playerScript != null) {
            playerScript.isDialogueActive = false; // Разблокировать движение персонажа
        }
    }

    private bool ValidateAnswer(string answer) {
        // Логика для проверки ответа
        return answer == "14"; // Пример правильного ответа
    }

    public void ExitResult() {
        AudioManager.Instance.PlayButtonSound();
        ResultWindow.SetActive(false); // Отключаем окно результата
    }
}

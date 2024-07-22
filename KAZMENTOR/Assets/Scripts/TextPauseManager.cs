using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Подключение библиотеки TextMesh Pro

public class TextPauseManager : MonoBehaviour {
    public GameObject canvas; // Ссылка на Canvas с текстами
    public GameObject canvasMenu; // Ссылка на Canvas с меню паузы
    private bool isPaused = false; // Текущий статус паузы

    void Start() {
        canvasMenu.SetActive(false); // Убеждаемся, что меню паузы выключено в начале игры
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.KeypadEnter)) // Допустим, что 'Enter' ставит игру на паузу
        {
            TogglePause();
        }
    }

    public void TogglePause() {
        isPaused = !isPaused;

        if (isPaused) {
            Time.timeScale = 0f; // Ставим время на паузу
            canvasMenu.SetActive(true); // Включаем Canvas с меню паузы
            SetTextVisibility(false); // Скрываем тексты
        } else {
            Time.timeScale = 1f; // Продолжаем время
            canvasMenu.SetActive(false); // Выключаем Canvas с меню паузы
            SetTextVisibility(true); // Показываем тексты
        }
    }

    private void SetTextVisibility(bool isVisible) {
        foreach (Transform child in canvas.transform) {
            if (child.GetComponent<TextMeshProUGUI>() != null) {
                child.gameObject.SetActive(isVisible);
            }
        }
    }
}

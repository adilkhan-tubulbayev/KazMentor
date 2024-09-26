using UnityEngine;
using TMPro;

public class StopwatchController : MonoBehaviour {
    public TextMeshProUGUI timerText; // Текст для отображения времени
    private float elapsedTime = 0f; // Прошедшее время
    private bool isRunning = false; // Флаг для отслеживания, запущен ли таймер

    // Методы для кнопок
    public void StartTimer() {
        isRunning = true;
    }

    public void StopTimer() {
        isRunning = false;
    }

    public void ResetTimer() {
        isRunning = false;
        elapsedTime = 0f;
        UpdateTimerDisplay(); // Обновляем отображение, чтобы сбросить время
    }

    void Update() {
        if (isRunning) {
            elapsedTime += Time.deltaTime; // Считаем прошедшее время
            UpdateTimerDisplay(); // Обновляем отображение
        }
    }

    // Обновление текста таймера
    private void UpdateTimerDisplay() {
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 100F) % 100F);
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}

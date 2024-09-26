using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Thermometer : MonoBehaviour {
    public RectTransform redBar; // Ссылка на красную полоску (RectTransform)
    public TextMeshProUGUI temperatureText; // Текстовое поле для отображения температуры
    public Button measureButton; // Кнопка измерения температуры

    private float roomTemperature = 0f; // Начальная комнатная температура
    private float targetTemperature; // Целевая температура (комнатная температура 20-25°C)
    private float maxTemperature = 50f; // Максимальная температура для термометра
    private float animationSpeed = 0.5f; // Скорость анимации подъёма полоски

    private float maxHeight = 200f; // Максимальная высота красной полоски (настрой под свой термометр)
    private float minHeight = 0f;   // Минимальная высота полоски (для 0 градусов)

    private bool isMeasuring = false; // Флаг, чтобы не запускать анимацию несколько раз

    void Start() {
        // Привязываем событие к кнопке "Measure Temperature"
        measureButton.onClick.AddListener(MeasureRoomTemperature);
        UpdateTemperatureDisplay(roomTemperature);
    }

    // Метод, который запускается при нажатии кнопки "Измерить температуру"
    public void MeasureRoomTemperature() {
        if (!isMeasuring) {
            targetTemperature = Random.Range(20f, 25f); // Генерация комнатной температуры в пределах 20-25°C
            StartCoroutine(AnimateTemperature(targetTemperature));
        }
    }

    // Анимация увеличения полоски до целевого значения температуры
    IEnumerator AnimateTemperature(float targetTemp) {
        isMeasuring = true; // Устанавливаем флаг, чтобы нельзя было повторно нажать кнопку во время анимации
        while (roomTemperature < targetTemp) {
            roomTemperature += Time.deltaTime * animationSpeed; // Плавно увеличиваем температуру
            UpdateTemperatureDisplay(roomTemperature); // Обновляем отображение температуры
            yield return null; // Ждём до следующего кадра
        }
        roomTemperature = targetTemp; // Зафиксировать точное значение после завершения анимации
        UpdateTemperatureDisplay(roomTemperature);
        isMeasuring = false; // Снимаем флаг после завершения анимации
    }

    // Обновление отображения температуры и полоски
    private void UpdateTemperatureDisplay(float temperature) {
        // Обновляем текст
        temperatureText.text = temperature.ToString("F1") + " °C";

        // Рассчитываем высоту красной полоски относительно текущей температуры
        float newHeight = Mathf.Lerp(minHeight, maxHeight, temperature / maxTemperature);
        redBar.sizeDelta = new Vector2(redBar.sizeDelta.x, newHeight); // Меняем только высоту полоски
    }
}

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Thermometer : MonoBehaviour {
    public RectTransform redBar; // Ссылка на красную полоску (RectTransform)
    public TextMeshProUGUI temperatureText; // Текстовое поле для отображения температуры
    public Button measureButton; // Кнопка измерения температуры
    public HeaterController heater; // Ссылка на нагреватель

    private float roomTemperature = 0f; // Начальная комнатная температура
    private float targetTemperature; // Целевая температура
    private float maxTemperature = 200f; // Максимальная температура для термометра
    private float minHeight = 0f;   // Минимальная высота полоски
    private float maxHeight = 200f; // Максимальная высота красной полоски
    private bool isMeasuring = false;

    void Start() {
        measureButton.onClick.AddListener(MeasureTemperature);
        UpdateTemperatureDisplay(roomTemperature);
    }

    public void MeasureTemperature() {
        if (!isMeasuring) {
            // Если нагреватель включен — нагреваем до 200°C
            if (heater.isHeating) {
                targetTemperature = Random.Range(160f, 200f);
            } else {
                targetTemperature = Random.Range(20f, 25f); // Комнатная температура
            }

            StartCoroutine(AnimateTemperature(targetTemperature));
        }
    }

    private IEnumerator AnimateTemperature(float targetTemp) {
        isMeasuring = true;
        float startTemp = roomTemperature;
        float elapsedTime = 0f;
        float duration = 20f;  // Задаем длительность нагрева

        while (elapsedTime < duration) {
            elapsedTime += Time.deltaTime;
            roomTemperature = Mathf.Lerp(startTemp, targetTemp, elapsedTime / duration);
            UpdateTemperatureDisplay(roomTemperature);
            yield return null;
        }

        roomTemperature = targetTemp;
        UpdateTemperatureDisplay(roomTemperature);
        isMeasuring = false;
    }

    public void UpdateTemperatureDisplay(float temperature) {
        temperatureText.text = temperature.ToString("F1") + " °C";
        float newHeight = Mathf.Lerp(minHeight, maxHeight, temperature / maxTemperature);
        redBar.sizeDelta = new Vector2(redBar.sizeDelta.x, newHeight);
    }
}

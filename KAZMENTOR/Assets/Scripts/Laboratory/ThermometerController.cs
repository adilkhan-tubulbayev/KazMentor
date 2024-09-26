using UnityEngine;
using TMPro;

public class ThermometerController : MonoBehaviour {
    public RectTransform redBar; // Ссылка на красную полоску
    public TextMeshProUGUI temperatureText; // Текст для отображения температуры
    private float maxHeight = 200f; // Максимальная высота красной полоски для 200°C

    public void UpdateThermometer(float temperature) {
        // Обновляем высоту красной полоски
        float newHeight = Mathf.Clamp(temperature / 200f * maxHeight, 0, maxHeight);
        redBar.sizeDelta = new Vector2(redBar.sizeDelta.x, newHeight);

        // Обновляем текст температуры
        temperatureText.text = temperature.ToString("F1") + " °C";
    }
}

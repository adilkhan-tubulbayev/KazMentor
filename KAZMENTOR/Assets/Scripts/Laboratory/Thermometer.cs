using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Thermometer : MonoBehaviour {
    public RectTransform redBar;
    public TextMeshProUGUI temperatureText;
    public Button measureButton;
    public HeaterController heater;

    private float roomTemperature = 0f;
    private float targetTemperature;
    private float maxTemperature = 200f;
    private float minHeight = 0f;
    private float maxHeight = 200f;
    private bool isMeasuring = false;

    void Start() {
        measureButton.onClick.AddListener(MeasureTemperature);
        UpdateTemperatureDisplay(roomTemperature);
    }

    public void MeasureTemperature() {
        if (!isMeasuring) {
            if (heater.isHeating) {
                targetTemperature = Random.Range(160f, 200f);
            } else {
                targetTemperature = Random.Range(20f, 25f);
            }

            AudioManager.Instance.PlayButtonSound(); // Воспроизведение звука

            StartCoroutine(AnimateTemperature(targetTemperature));
        }
    }

    private IEnumerator AnimateTemperature(float targetTemp) {
        isMeasuring = true;
        float startTemp = roomTemperature;
        float elapsedTime = 0f;
        float duration = 20f;

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

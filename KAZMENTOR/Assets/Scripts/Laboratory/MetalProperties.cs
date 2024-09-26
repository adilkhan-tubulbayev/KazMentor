using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // Для работы с Image

public class MetalProperties : MonoBehaviour {
    public float mass = 1f; // Масса металла (в кг)
    public float specificHeat = 0.5f; // Удельная теплоёмкость
    public float currentTemperature = 20f; // Начальная температура (в °C)
    public float maxTemperature = 200f;  // Максимальная температура
    public Color normalColor = Color.gray; // Цвет при комнатной температуре
    public Color heatedColor = Color.red;  // Цвет при максимальной температуре

    private Image image; // Используем Image для изменения цвета
    private bool isHeating = false;
    private float heatingDuration = 20f; // Время для нагрева до красного цвета

    private void Awake() {
        image = GetComponent<Image>();  // Получаем компонент Image для изменения цвета
        if (image == null) {
            Debug.LogError("Отсутствует компонент Image на объекте " + gameObject.name);
        }
    }

    private void Update() {
        if (currentTemperature > 20f && !isHeating) {
            CoolDown(1f);  // Охлаждение металла
        }
    }

    // Метод для обновления температуры и запуска нагрева
    public void UpdateTemperature(float energy) {
        if (!isHeating) {
            StartCoroutine(HeatOverTime(energy));
        }
    }

    private IEnumerator HeatOverTime(float energy) {
        isHeating = true;
        float startTime = Time.time;
        float startTemperature = currentTemperature;
        float targetTemperature = maxTemperature;
        float duration = heatingDuration;

        while (Time.time < startTime + duration) {
            float elapsed = Time.time - startTime;
            float percentageComplete = elapsed / duration;

            // Увеличение температуры
            currentTemperature = Mathf.Lerp(startTemperature, targetTemperature, percentageComplete);

            // Обновляем цвет, не изменяя прозрачность (alpha)
            UpdateColor();

            yield return null;
        }

        currentTemperature = maxTemperature;
        UpdateColor();
        isHeating = false;
    }

    private void UpdateColor() {
        if (image != null) {
            float temperatureRatio = Mathf.Clamp01(currentTemperature / maxTemperature);

            // Сохраняем исходное значение альфа-канала (прозрачности)
            Color newColor = Color.Lerp(normalColor, heatedColor, temperatureRatio);
            newColor.a = image.color.a; // Сохраняем текущую прозрачность
            image.color = newColor;
        }
    }

    public void CoolDown(float coolingRate) {
        currentTemperature = Mathf.Max(currentTemperature - coolingRate * Time.deltaTime, 20f);
        UpdateColor();  // Обновляем цвет в процессе охлаждения
    }
}

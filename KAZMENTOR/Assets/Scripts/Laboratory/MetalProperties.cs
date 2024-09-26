using UnityEngine;

public class MetalProperties : MonoBehaviour {
    public float mass = 1f; // Масса металла (в кг)
    public float specificHeat = 0.5f; // Удельная теплоёмкость (уменьшена для быстрого нагрева)
    public float currentTemperature = 20f; // Начальная температура (в °C)

    // Цвет металла при комнатной температуре
    public Color normalColor = Color.gray;

    // Цвет металла при нагреве
    public Color heatedColor = Color.red;

    // Максимальная температура для данного металла
    public float maxTemperature = 200f;

    private SpriteRenderer spriteRenderer;  // Компонент для изменения цвета спрайта

    private void Awake() {
        // Пытаемся получить компонент SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Если спрайтрендера нет — выводим ошибку в консоль
        if (spriteRenderer == null) {
            Debug.LogError("Отсутствует компонент SpriteRenderer на объекте " + gameObject.name);
        } else {
            spriteRenderer.color = normalColor; // Устанавливаем начальный цвет металла
        }
    }

    public void UpdateTemperature(float energy) {
        // Увеличение температуры
        float deltaTemperature = energy / (mass * specificHeat);
        currentTemperature += deltaTemperature;

        // Ограничиваем максимальную температуру
        currentTemperature = Mathf.Clamp(currentTemperature, 20f, maxTemperature);

        // Обновление цвета в зависимости от температуры
        UpdateColor();
    }

    private void UpdateColor() {
        // Если есть SpriteRenderer, изменяем цвет в зависимости от температуры
        if (spriteRenderer != null) {
            float temperatureRatio = Mathf.Clamp01(currentTemperature / maxTemperature);
            spriteRenderer.color = Color.Lerp(normalColor, heatedColor, temperatureRatio);
        }
    }

    public void CoolDown(float coolingRate) {
        // Постепенное охлаждение
        currentTemperature = Mathf.Max(currentTemperature - coolingRate * Time.deltaTime, 20f);  // Минимум 20°C
        UpdateColor();  // Обновляем цвет в процессе охлаждения
    }
}

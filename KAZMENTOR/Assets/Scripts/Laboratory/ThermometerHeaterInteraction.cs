using UnityEngine;

public class ThermometerHeaterInteraction : MonoBehaviour {
    public HeaterController heater;       // Ссылка на нагреватель
    public ThermometerController thermometer;  // Ссылка на термометр
    public MetalProperties[] metals;      // Массив всех металлов в сцене

    private void Update() {
        if (heater.isHeating) {
            float maxTemperature = 0f;

            // Определяем максимальную температуру среди всех металлов
            foreach (MetalProperties metal in metals) {
                if (metal.currentTemperature > maxTemperature) {
                    maxTemperature = metal.currentTemperature;
                }
            }

            // Обновляем термометр
            thermometer.UpdateThermometer(maxTemperature);
        }
    }
}

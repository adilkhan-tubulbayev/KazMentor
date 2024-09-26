using UnityEngine;

public class ThermometerHeaterInteraction : MonoBehaviour {
    public HeaterController heater;       // Ссылка на нагреватель
    public Thermometer thermometer;  // Ссылка на термометр
    public MetalProperties[] metals;      // Массив всех металлов

    private void Update() {
        if (heater.isHeating) {
            float maxTemperature = 0f;
            foreach (MetalProperties metal in metals) {
                if (metal.currentTemperature < metal.maxTemperature) {
                    metal.UpdateTemperature(heater.heatingPower * Time.deltaTime);  // Нагреваем металл
                }
                if (metal.currentTemperature > maxTemperature) {
                    maxTemperature = metal.currentTemperature;
                }
            }

            // Обновляем термометр с максимальной температурой
            thermometer.UpdateTemperatureDisplay(maxTemperature);
        }
    }
}

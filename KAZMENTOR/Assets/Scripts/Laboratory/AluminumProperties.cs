using UnityEngine;

public class AluminumProperties : MonoBehaviour {
    public float mass = 1f; // Масса алюминия (в кг)
    public float specificHeat = 0.897f; // Удельная теплоёмкость алюминия (Дж/(кг·°C))
    public float currentTemperature = 20f; // Начальная температура (в °C)

    public void UpdateTemperature(float energy) {
        float deltaTemperature = energy / (mass * specificHeat);
        currentTemperature += deltaTemperature;
    }
}

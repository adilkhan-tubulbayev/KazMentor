using UnityEngine;

public class PlumbumProperties : MonoBehaviour
{
    public float mass = 1f; // Масса свинца (в кг)
    public float specificHeat = 0.128f; // Удельная теплоёмкость свинца (Дж/(кг·°C))
    public float currentTemperature = 20f; // Начальная температура (в °C)

    public void UpdateTemperature(float energy) {
        float deltaTemperature = energy / (mass * specificHeat);
        currentTemperature += deltaTemperature;
    }
}

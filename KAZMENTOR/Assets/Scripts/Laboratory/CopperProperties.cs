using UnityEngine;

public class CopperProperties : MonoBehaviour {
    public float mass = 1f; // Масса меди (в кг)
    public float specificHeat = 0.385f; // Удельная теплоёмкость меди (Дж/(кг·°C))
    public float currentTemperature = 20f; // Начальная температура (в °C)

    public void UpdateTemperature(float energy) {
        // ΔT = Q / (m * c)
        float deltaTemperature = energy / (mass * specificHeat);
        currentTemperature += deltaTemperature;
    }
}

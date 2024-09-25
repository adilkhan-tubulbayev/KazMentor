using UnityEngine;

public class AluminumProperties : MonoBehaviour {
    public float mass = 1f; // ����� �������� (� ��)
    public float specificHeat = 0.897f; // �������� ����������� �������� (��/(�㷰C))
    public float currentTemperature = 20f; // ��������� ����������� (� �C)

    public void UpdateTemperature(float energy) {
        float deltaTemperature = energy / (mass * specificHeat);
        currentTemperature += deltaTemperature;
    }
}

using UnityEngine;

public class PlumbumProperties : MonoBehaviour
{
    public float mass = 1f; // ����� ������ (� ��)
    public float specificHeat = 0.128f; // �������� ����������� ������ (��/(�㷰C))
    public float currentTemperature = 20f; // ��������� ����������� (� �C)

    public void UpdateTemperature(float energy) {
        float deltaTemperature = energy / (mass * specificHeat);
        currentTemperature += deltaTemperature;
    }
}

using UnityEngine;

public class ThermometerHeaterInteraction : MonoBehaviour {
    public HeaterController heater;       // ������ �� �����������
    public ThermometerController thermometer;  // ������ �� ���������
    public MetalProperties[] metals;      // ������ ���� �������� � �����

    private void Update() {
        if (heater.isHeating) {
            float maxTemperature = 0f;

            // ���������� ������������ ����������� ����� ���� ��������
            foreach (MetalProperties metal in metals) {
                if (metal.currentTemperature > maxTemperature) {
                    maxTemperature = metal.currentTemperature;
                }
            }

            // ��������� ���������
            thermometer.UpdateThermometer(maxTemperature);
        }
    }
}

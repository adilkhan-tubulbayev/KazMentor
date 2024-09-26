using UnityEngine;

public class ThermometerHeaterInteraction : MonoBehaviour {
    public HeaterController heater;       // ������ �� �����������
    public Thermometer thermometer;  // ������ �� ���������
    public MetalProperties[] metals;      // ������ ���� ��������

    private void Update() {
        if (heater.isHeating) {
            float maxTemperature = 0f;
            foreach (MetalProperties metal in metals) {
                if (metal.currentTemperature < metal.maxTemperature) {
                    metal.UpdateTemperature(heater.heatingPower * Time.deltaTime);  // ��������� ������
                }
                if (metal.currentTemperature > maxTemperature) {
                    maxTemperature = metal.currentTemperature;
                }
            }

            // ��������� ��������� � ������������ ������������
            thermometer.UpdateTemperatureDisplay(maxTemperature);
        }
    }
}

using UnityEngine;
using TMPro;

public class ThermometerController : MonoBehaviour {
    public RectTransform redBar; // ������ �� ������� �������
    public TextMeshProUGUI temperatureText; // ����� ��� ����������� �����������
    private float maxHeight = 200f; // ������������ ������ ������� ������� ��� 200�C

    public void UpdateThermometer(float temperature) {
        // ��������� ������ ������� �������
        float newHeight = Mathf.Clamp(temperature / 200f * maxHeight, 0, maxHeight);
        redBar.sizeDelta = new Vector2(redBar.sizeDelta.x, newHeight);

        // ��������� ����� �����������
        temperatureText.text = temperature.ToString("F1") + " �C";
    }
}

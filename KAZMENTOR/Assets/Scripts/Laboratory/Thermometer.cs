using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Thermometer : MonoBehaviour {
    public RectTransform redBar; // ������ �� ������� ������� (RectTransform)
    public TextMeshProUGUI temperatureText; // ��������� ���� ��� ����������� �����������
    public Button measureButton; // ������ ��������� �����������

    private float roomTemperature = 0f; // ��������� ��������� �����������
    private float targetTemperature; // ������� ����������� (��������� ����������� 20-25�C)
    private float maxTemperature = 50f; // ������������ ����������� ��� ����������
    private float animationSpeed = 0.5f; // �������� �������� ������� �������

    private float maxHeight = 200f; // ������������ ������ ������� ������� (������� ��� ���� ���������)
    private float minHeight = 0f;   // ����������� ������ ������� (��� 0 ��������)

    private bool isMeasuring = false; // ����, ����� �� ��������� �������� ��������� ���

    void Start() {
        // ����������� ������� � ������ "Measure Temperature"
        measureButton.onClick.AddListener(MeasureRoomTemperature);
        UpdateTemperatureDisplay(roomTemperature);
    }

    // �����, ������� ����������� ��� ������� ������ "�������� �����������"
    public void MeasureRoomTemperature() {
        if (!isMeasuring) {
            targetTemperature = Random.Range(20f, 25f); // ��������� ��������� ����������� � �������� 20-25�C
            StartCoroutine(AnimateTemperature(targetTemperature));
        }
    }

    // �������� ���������� ������� �� �������� �������� �����������
    IEnumerator AnimateTemperature(float targetTemp) {
        isMeasuring = true; // ������������� ����, ����� ������ ���� �������� ������ ������ �� ����� ��������
        while (roomTemperature < targetTemp) {
            roomTemperature += Time.deltaTime * animationSpeed; // ������ ����������� �����������
            UpdateTemperatureDisplay(roomTemperature); // ��������� ����������� �����������
            yield return null; // ��� �� ���������� �����
        }
        roomTemperature = targetTemp; // ������������� ������ �������� ����� ���������� ��������
        UpdateTemperatureDisplay(roomTemperature);
        isMeasuring = false; // ������� ���� ����� ���������� ��������
    }

    // ���������� ����������� ����������� � �������
    private void UpdateTemperatureDisplay(float temperature) {
        // ��������� �����
        temperatureText.text = temperature.ToString("F1") + " �C";

        // ������������ ������ ������� ������� ������������ ������� �����������
        float newHeight = Mathf.Lerp(minHeight, maxHeight, temperature / maxTemperature);
        redBar.sizeDelta = new Vector2(redBar.sizeDelta.x, newHeight); // ������ ������ ������ �������
    }
}

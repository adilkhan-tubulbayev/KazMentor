using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // ��� ������ � Image

public class MetalProperties : MonoBehaviour {
    public float mass = 1f; // ����� ������� (� ��)
    public float specificHeat = 0.5f; // �������� �����������
    public float currentTemperature = 20f; // ��������� ����������� (� �C)
    public float maxTemperature = 200f;  // ������������ �����������
    public Color normalColor = Color.gray; // ���� ��� ��������� �����������
    public Color heatedColor = Color.red;  // ���� ��� ������������ �����������

    private Image image; // ���������� Image ��� ��������� �����
    private bool isHeating = false;
    private float heatingDuration = 20f; // ����� ��� ������� �� �������� �����

    private void Awake() {
        image = GetComponent<Image>();  // �������� ��������� Image ��� ��������� �����
        if (image == null) {
            Debug.LogError("����������� ��������� Image �� ������� " + gameObject.name);
        }
    }

    private void Update() {
        if (currentTemperature > 20f && !isHeating) {
            CoolDown(1f);  // ���������� �������
        }
    }

    // ����� ��� ���������� ����������� � ������� �������
    public void UpdateTemperature(float energy) {
        if (!isHeating) {
            StartCoroutine(HeatOverTime(energy));
        }
    }

    private IEnumerator HeatOverTime(float energy) {
        isHeating = true;
        float startTime = Time.time;
        float startTemperature = currentTemperature;
        float targetTemperature = maxTemperature;
        float duration = heatingDuration;

        while (Time.time < startTime + duration) {
            float elapsed = Time.time - startTime;
            float percentageComplete = elapsed / duration;

            // ���������� �����������
            currentTemperature = Mathf.Lerp(startTemperature, targetTemperature, percentageComplete);

            // ��������� ����, �� ������� ������������ (alpha)
            UpdateColor();

            yield return null;
        }

        currentTemperature = maxTemperature;
        UpdateColor();
        isHeating = false;
    }

    private void UpdateColor() {
        if (image != null) {
            float temperatureRatio = Mathf.Clamp01(currentTemperature / maxTemperature);

            // ��������� �������� �������� �����-������ (������������)
            Color newColor = Color.Lerp(normalColor, heatedColor, temperatureRatio);
            newColor.a = image.color.a; // ��������� ������� ������������
            image.color = newColor;
        }
    }

    public void CoolDown(float coolingRate) {
        currentTemperature = Mathf.Max(currentTemperature - coolingRate * Time.deltaTime, 20f);
        UpdateColor();  // ��������� ���� � �������� ����������
    }
}

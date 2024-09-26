using UnityEngine;

public class MetalProperties : MonoBehaviour {
    public float mass = 1f; // ����� ������� (� ��)
    public float specificHeat = 0.5f; // �������� ����������� (��������� ��� �������� �������)
    public float currentTemperature = 20f; // ��������� ����������� (� �C)

    // ���� ������� ��� ��������� �����������
    public Color normalColor = Color.gray;

    // ���� ������� ��� �������
    public Color heatedColor = Color.red;

    // ������������ ����������� ��� ������� �������
    public float maxTemperature = 200f;

    private SpriteRenderer spriteRenderer;  // ��������� ��� ��������� ����� �������

    private void Awake() {
        // �������� �������� ��������� SpriteRenderer
        spriteRenderer = GetComponent<SpriteRenderer>();

        // ���� ������������� ��� � ������� ������ � �������
        if (spriteRenderer == null) {
            Debug.LogError("����������� ��������� SpriteRenderer �� ������� " + gameObject.name);
        } else {
            spriteRenderer.color = normalColor; // ������������� ��������� ���� �������
        }
    }

    public void UpdateTemperature(float energy) {
        // ���������� �����������
        float deltaTemperature = energy / (mass * specificHeat);
        currentTemperature += deltaTemperature;

        // ������������ ������������ �����������
        currentTemperature = Mathf.Clamp(currentTemperature, 20f, maxTemperature);

        // ���������� ����� � ����������� �� �����������
        UpdateColor();
    }

    private void UpdateColor() {
        // ���� ���� SpriteRenderer, �������� ���� � ����������� �� �����������
        if (spriteRenderer != null) {
            float temperatureRatio = Mathf.Clamp01(currentTemperature / maxTemperature);
            spriteRenderer.color = Color.Lerp(normalColor, heatedColor, temperatureRatio);
        }
    }

    public void CoolDown(float coolingRate) {
        // ����������� ����������
        currentTemperature = Mathf.Max(currentTemperature - coolingRate * Time.deltaTime, 20f);  // ������� 20�C
        UpdateColor();  // ��������� ���� � �������� ����������
    }
}

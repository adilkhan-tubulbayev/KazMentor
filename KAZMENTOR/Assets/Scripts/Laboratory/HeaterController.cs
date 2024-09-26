using UnityEngine;

public class HeaterController : MonoBehaviour {
    public float heatingRange = 1.5f;  // ������ �������� �����������
    public float heatingPower = 100f;  // �������� �����������
    public bool isHeating = false;     // ����, ������� �� �����������
    public Animator heaterAnimator;    // ������ �� �������� �����������

    private void Update() {
        if (isHeating) {
            // ��������� ��� ������� � MetalProperties � ������� �������� �����������
            MetalProperties[] metals = FindObjectsOfType<MetalProperties>();
            foreach (MetalProperties metal in metals) {
                if (Vector3.Distance(transform.position, metal.transform.position) <= heatingRange) {
                    metal.UpdateTemperature(heatingPower * Time.deltaTime);  // ��������� ������
                }
            }
        }
    }

    public void StartHeating() {
        isHeating = true;
        heaterAnimator.SetBool("IsHeating", true);  // �������� �������� �������
    }

    public void StopHeating() {
        isHeating = false;
        heaterAnimator.SetBool("IsHeating", false);  // ��������� �������� �������
    }
}

using UnityEngine;
using TMPro;

public class CircuitChecker : MonoBehaviour {
    public DropZone[] dropZones; // ������ �� ��� ���� ������
    public string[] correctTags; // ������ ���������� ����� ��� ��� ������
    public TextMeshProUGUI resultText; // ����� ��� ����������� ����������
    public GameObject circuitResult; // ������ ��� ����������� ����������
    public BarrierController barrierController; // ��������� ������ �� BarrierController

    private Vector3[] initialPositions; // �������� ������� ��������������� ��������
    private DraggableItem[] draggableItems; // ��������������� �������
    private bool isCircuitCorrect = false; // ���� ��� ������������ ������������ �����

    private void Awake() {
        circuitResult.SetActive(false); // ������ ������ CircuitResult ��� ������ ����

        // �������� �������� ������� ���� ��������������� ��������
        draggableItems = FindObjectsOfType<DraggableItem>();
        initialPositions = new Vector3[draggableItems.Length];

        for (int i = 0; i < draggableItems.Length; i++) {
            initialPositions[i] = draggableItems[i].transform.position;
        }
    }

    public void CheckCircuit() {
        AudioManager.Instance.PlayButtonSound();
        bool isCorrect = true;

        // �������� ������ ���� ������
        for (int i = 0; i < dropZones.Length; i++) {
            bool zoneIsCorrect = dropZones[i].HasCorrectItem(correctTags[i]);
            Debug.Log($"�������� ���� {dropZones[i].name}: {zoneIsCorrect} (���������: {correctTags[i]})");
            if (!zoneIsCorrect) {
                isCorrect = false;
                break;
            }
        }

        // ����������� ����������
        if (isCorrect) {
            isCircuitCorrect = true; // ������������� ���� ������������ �����
            resultText.text = "����� ������� �����!";
            Debug.Log("����� ������� �����!");
        } else {
            isCircuitCorrect = false; // ���������� ���� ������������ �����
            resultText.text = "����� ������� �������.";
            Debug.Log("����� ������� �������.");
        }

        circuitResult.SetActive(true); // �������� ���������

        if (barrierController != null) {
            barrierController.SetCircuitCorrect(isCorrect);
        }
    }

    public bool IsCircuitCorrect() {
        return isCircuitCorrect;
    }

    public void ExitResult() {
        AudioManager.Instance.PlayButtonSound();
        circuitResult.SetActive(false); // ��������� ���� ����������

        // ���������� ��������������� ������� �� �� �������� ������� ������ ���� ����� �������
        if (!isCircuitCorrect) {
            ResetDraggableItems();
        }

        if (isCircuitCorrect) {
            barrierController.SetCircuitCorrect(true);
        }
    }

    private void ResetDraggableItems() {
        for (int i = 0; i < draggableItems.Length; i++) {
            draggableItems[i].transform.position = initialPositions[i];
            draggableItems[i].transform.SetParent(draggableItems[i].originalParent); // ���������� ��������� ��������
            draggableItems[i].ClearDroppedZone(); // �������� ������� ���� ������
        }
    }
}

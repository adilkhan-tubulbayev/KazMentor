using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // ����������� ���������� TextMesh Pro

public class TextPauseManager : MonoBehaviour {
    public GameObject canvas; // ������ �� Canvas � ��������
    public GameObject canvasMenu; // ������ �� Canvas � ���� �����
    private bool isPaused = false; // ������� ������ �����

    void Start() {
        canvasMenu.SetActive(false); // ����������, ��� ���� ����� ��������� � ������ ����
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.KeypadEnter)) // ��������, ��� 'Enter' ������ ���� �� �����
        {
            TogglePause();
        }
    }

    public void TogglePause() {
        isPaused = !isPaused;

        if (isPaused) {
            Time.timeScale = 0f; // ������ ����� �� �����
            canvasMenu.SetActive(true); // �������� Canvas � ���� �����
            SetTextVisibility(false); // �������� ������
        } else {
            Time.timeScale = 1f; // ���������� �����
            canvasMenu.SetActive(false); // ��������� Canvas � ���� �����
            SetTextVisibility(true); // ���������� ������
        }
    }

    private void SetTextVisibility(bool isVisible) {
        foreach (Transform child in canvas.transform) {
            if (child.GetComponent<TextMeshProUGUI>() != null) {
                child.gameObject.SetActive(isVisible);
            }
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraTransitionReverse : MonoBehaviour {
    public Transform player; // �����, � �������� ����� ���������� ������
    public float targetOrthographicSize = 7f; // ������� ������ ��������������� ������ (���� ������ ���������������)
    public float zoomSpeed = 2f; // �������� ���������� ������� ������
    public float zoomDuration = 2f; // ����� �� ���������� ������� ������
    public Image fadeImage; // Image ��� ���������� ������
    public float fadeDuration = 2f; // ������������ ����������
    public CameraFollow cameraFollow; // ������ �� ������ CameraFollow

    private bool isTransitioning = false;
    private float initialOrthographicSize;
    private float timeElapsed = 0f;

    void Start() {
        // ��������� ���������� ������ �� ���������� �� ����� ���������� �������
        if (cameraFollow != null) {
            cameraFollow.enabled = false;
        }

        // ������������� ������ �� ��������� ������� � ������� ������
        Camera.main.transform.position = new Vector3(player.position.x, player.position.y, Camera.main.transform.position.z);

        // ���������� ��������� ������ ������
        initialOrthographicSize = Camera.main.orthographicSize;

        // �������� � ��������� ������� ������
        fadeImage.color = new Color(0, 0, 0, 1f);

        // ������ ���������� ������� ������ � ���������� ������
        StartTransitionWithFade();
    }

    void Update() {
        if (isTransitioning) {
            // �������� ������������ ������� ������
            Camera.main.orthographicSize = Mathf.Lerp(initialOrthographicSize, targetOrthographicSize, timeElapsed / zoomDuration);

            // �������� ������������ ��� ���������� ������ (�����-�����)
            float fadeProgress = Mathf.Lerp(1f, 0f, timeElapsed / fadeDuration); // ���������� ��������� � ����������� �������
            fadeImage.color = new Color(0, 0, 0, fadeProgress);

            timeElapsed += Time.deltaTime;

            // ������������� ���������� ������� ������ � ����������, ����� ����� �����
            if (timeElapsed >= zoomDuration) {
                isTransitioning = false;

                // ������� ����������� ��������� ����� ����������
                fadeImage.color = new Color(0, 0, 0, 0f);
                fadeImage.raycastTarget = false; // ������ ����������� �������������� ����� ����������
                fadeImage.gameObject.SetActive(false); // ��������� fadeImage ����� ���������� ����������

                // �������� ������� ���������� ������ �� ����������
                if (cameraFollow != null) {
                    cameraFollow.enabled = true;
                }
            }
        }
    }

    public void StartTransitionWithFade() {
        // ������������� �������� � ������ ���������� ������� ������ � �����������
        isTransitioning = true;
        timeElapsed = 0f;
    }
}

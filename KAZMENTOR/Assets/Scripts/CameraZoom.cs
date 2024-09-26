using System.Collections;
using UnityEngine;

public class CameraZoom : MonoBehaviour {
    public CameraFollow cameraFollow; // ������ �� ������ CameraFollow
    public float zoomedSize = 10f; // ������� ������ ������ ��� ����������
    public float zoomDuration = 3f; // �����, �� ������� ������ ������ �������������
    public float zoomBackDuration = 2f; // �����, �� ������� ������ ������ ������������ � ������������� �������

    private Camera mainCamera;
    private float originalSize; // �������� ������ ������
    private bool isZooming = false; // ���� ��� ��������, ��� ��� �������

    void Start() {
        // �������� ������ �� ������ � ��������� �� �������� ������
        mainCamera = Camera.main;
        originalSize = mainCamera.orthographicSize;

        // ��������� ����������� ������ ����� ��� ������
        ZoomIn();
    }

    public void ZoomIn() {
        if (!isZooming) {
            StartCoroutine(ZoomEffect(zoomedSize, zoomDuration, false)); // ������� ��� �� ������� ������
        }
    }

    private IEnumerator ZoomEffect(float targetSize, float duration, bool isReturning) {
        isZooming = true;

        // ��������� ������ CameraFollow, ����� �� ����� �����������
        if (!isReturning && cameraFollow != null) {
            cameraFollow.enabled = false;
        }

        float timeElapsed = 0f;
        float startSize = mainCamera.orthographicSize;

        // ������� ������� �� �������� ������� � ��������
        while (timeElapsed < duration) {
            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // ��������, ��� ������ ����� ���� �������
        mainCamera.orthographicSize = targetSize;

        if (!isReturning) {
            // ����� ���������� ���� ��� ��������� ������, ����� ������������ �������
            yield return new WaitForSeconds(zoomDuration); // �������� ����� ���������
            StartCoroutine(ZoomEffect(originalSize, zoomBackDuration, true)); // ������� ������� � ��������� �������
        } else {
            // ����� �������� �������� CameraFollow
            if (cameraFollow != null) {
                cameraFollow.enabled = true;
            }
            isZooming = false;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // ���������, ����� ������� �����
using System.Collections;

public class CameraTransition : MonoBehaviour {
    public Transform targetPosition; // ������� ������� ��� ������
    public float zoomSpeed = 2f; // �������� �����������
    public float targetOrthographicSize = 5f; // ������� ������ ��������������� ������
    public float zoomDuration = 2f; // �����, �� ������� ������ ������������
    public float stopDistance = 10f; // ����������� ���������� �� ����
    public Image fadeImage; // Image ��� ����������
    public float fadeDuration = 2f; // ������������ ���������� (����� ����� ������� �����������)
    public string nextSceneName; // �������� ��������� �����

    private bool isTransitioning = false;
    private float initialOrthographicSize;
    private Vector3 initialPosition;
    private float timeElapsed = 0f;

    void Start() {
        // ��������� ��������� �������� ������
        initialOrthographicSize = Camera.main.orthographicSize;
        initialPosition = Camera.main.transform.position;
    }

    void Update() {
        if (isTransitioning) {
            // ������������ ������� ���������� �� ����
            float distanceToTarget = Vector3.Distance(Camera.main.transform.position, targetPosition.position);

            // ���� ���������� ������ ������������, ���������� �����������
            if (distanceToTarget > stopDistance) {
                // �������� ������������ ������� ������
                Camera.main.transform.position = Vector3.Lerp(initialPosition, targetPosition.position, timeElapsed / zoomDuration);

                // �������� ������������ ������� ������
                Camera.main.orthographicSize = Mathf.Lerp(initialOrthographicSize, targetOrthographicSize, timeElapsed / zoomDuration);

                // �������� ������������ ��� ���������� (�����-�����)
                float fadeProgress = Mathf.Lerp(0f, 1f, timeElapsed / zoomDuration); // ���������� ��������� � �����
                fadeImage.color = new Color(0, 0, 0, fadeProgress);

                timeElapsed += Time.deltaTime;

                // ������������� ���, ����� ����� ����������� ������
                if (timeElapsed >= zoomDuration) {
                    // ��������� � ��������� ����� ����� ������� ����������
                    StartCoroutine(LoadNextScene());
                }
            }
        }
    }

    public void StartTransitionWithFade() {
        // ������������� �������� � ������ ����������� � �����������
        isTransitioning = true;
        timeElapsed = 0f;
        initialPosition = Camera.main.transform.position; // ���������� ��������� ������� ������
        initialOrthographicSize = Camera.main.orthographicSize; // ���������� ��������� ������ ������

        // ��������������� ����� ��������
        AudioManager.Instance.PlayTransitionSound();
    }

    IEnumerator LoadNextScene() {
        // ��� ������� ����� ����, ��� ����� ��������� ����������
        yield return new WaitForSeconds(0.5f);

        // ��������� ��������� �����
        SceneManager.LoadScene("SchoolOutside");
    }
}

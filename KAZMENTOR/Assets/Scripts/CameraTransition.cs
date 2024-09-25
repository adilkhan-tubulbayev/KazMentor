using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Добавляем, чтобы сменить сцену
using System.Collections;

public class CameraTransition : MonoBehaviour {
    public Transform targetPosition; // Целевая позиция для камеры
    public float zoomSpeed = 2f; // Скорость приближения
    public float targetOrthographicSize = 5f; // Целевой размер ортографической камеры
    public float zoomDuration = 2f; // Время, за которое камера приближается
    public float stopDistance = 10f; // Минимальное расстояние до цели
    public Image fadeImage; // Image для затемнения
    public float fadeDuration = 2f; // Длительность затемнения (будет равна времени зумирования)
    public string nextSceneName; // Название следующей сцены

    private bool isTransitioning = false;
    private float initialOrthographicSize;
    private Vector3 initialPosition;
    private float timeElapsed = 0f;

    void Start() {
        // Сохраняем начальные значения камеры
        initialOrthographicSize = Camera.main.orthographicSize;
        initialPosition = Camera.main.transform.position;
    }

    void Update() {
        if (isTransitioning) {
            // Рассчитываем текущее расстояние до цели
            float distanceToTarget = Vector3.Distance(Camera.main.transform.position, targetPosition.position);

            // Если расстояние больше минимального, продолжаем приближение
            if (distanceToTarget > stopDistance) {
                // Линейная интерполяция позиции камеры
                Camera.main.transform.position = Vector3.Lerp(initialPosition, targetPosition.position, timeElapsed / zoomDuration);

                // Линейная интерполяция размера камеры
                Camera.main.orthographicSize = Mathf.Lerp(initialOrthographicSize, targetOrthographicSize, timeElapsed / zoomDuration);

                // Линейная интерполяция для затемнения (альфа-канал)
                float fadeProgress = Mathf.Lerp(0f, 1f, timeElapsed / zoomDuration); // затемнение синхронно с зумом
                fadeImage.color = new Color(0, 0, 0, fadeProgress);

                timeElapsed += Time.deltaTime;

                // Останавливаем зум, когда время зумирования прошло
                if (timeElapsed >= zoomDuration) {
                    // Переходим к следующей сцене после полного затемнения
                    StartCoroutine(LoadNextScene());
                }
            }
        }
    }

    public void StartTransitionWithFade() {
        // Инициализация значений и запуск зумирования с затемнением
        isTransitioning = true;
        timeElapsed = 0f;
        initialPosition = Camera.main.transform.position; // Запоминаем начальную позицию камеры
        initialOrthographicSize = Camera.main.orthographicSize; // Запоминаем начальный размер камеры

        // Воспроизведение звука перехода
        AudioManager.Instance.PlayTransitionSound();
    }

    IEnumerator LoadNextScene() {
        // Ждём немного после того, как экран полностью затемнился
        yield return new WaitForSeconds(0.5f);

        // Загружаем следующую сцену
        SceneManager.LoadScene("SchoolOutside");
    }
}

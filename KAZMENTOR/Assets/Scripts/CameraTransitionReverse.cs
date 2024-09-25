using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraTransitionReverse : MonoBehaviour {
    public Transform player; // Игрок, с которого будет начинаться камера
    public float targetOrthographicSize = 7f; // Целевой размер ортографической камеры (если камера ортографическая)
    public float zoomSpeed = 2f; // Скорость увеличения размера камеры
    public float zoomDuration = 2f; // Время на увеличение размера камеры
    public Image fadeImage; // Image для осветления экрана
    public float fadeDuration = 2f; // Длительность осветления
    public CameraFollow cameraFollow; // Ссылка на скрипт CameraFollow

    private bool isTransitioning = false;
    private float initialOrthographicSize;
    private float timeElapsed = 0f;

    void Start() {
        // Отключаем следование камеры за персонажем на время увеличения размера
        if (cameraFollow != null) {
            cameraFollow.enabled = false;
        }

        // Устанавливаем камеру на стартовую позицию — позицию игрока
        Camera.main.transform.position = new Vector3(player.position.x, player.position.y, Camera.main.transform.position.z);

        // Запоминаем начальный размер камеры
        initialOrthographicSize = Camera.main.orthographicSize;

        // Начинаем с полностью черного экрана
        fadeImage.color = new Color(0, 0, 0, 1f);

        // Запуск увеличения размера камеры и осветления экрана
        StartTransitionWithFade();
    }

    void Update() {
        if (isTransitioning) {
            // Линейная интерполяция размера камеры
            Camera.main.orthographicSize = Mathf.Lerp(initialOrthographicSize, targetOrthographicSize, timeElapsed / zoomDuration);

            // Линейная интерполяция для осветления экрана (альфа-канал)
            float fadeProgress = Mathf.Lerp(1f, 0f, timeElapsed / fadeDuration); // Осветление синхронно с увеличением размера
            fadeImage.color = new Color(0, 0, 0, fadeProgress);

            timeElapsed += Time.deltaTime;

            // Останавливаем увеличение размера камеры и осветление, когда время вышло
            if (timeElapsed >= zoomDuration) {
                isTransitioning = false;

                // Убираем изображение полностью после осветления
                fadeImage.color = new Color(0, 0, 0, 0f);
                fadeImage.raycastTarget = false; // Делаем изображение некликабельным после завершения
                fadeImage.gameObject.SetActive(false); // Отключаем fadeImage после завершения осветления

                // Включаем обратно следование камеры за персонажем
                if (cameraFollow != null) {
                    cameraFollow.enabled = true;
                }
            }
        }
    }

    public void StartTransitionWithFade() {
        // Инициализация значений и запуск увеличения размера камеры с осветлением
        isTransitioning = true;
        timeElapsed = 0f;
    }
}

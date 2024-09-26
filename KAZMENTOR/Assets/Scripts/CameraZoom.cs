using System.Collections;
using UnityEngine;

public class CameraZoom : MonoBehaviour {
    public CameraFollow cameraFollow; // Ссылка на скрипт CameraFollow
    public float zoomedSize = 10f; // Целевой размер камеры при увеличении
    public float zoomDuration = 3f; // Время, за которое камера плавно увеличивается
    public float zoomBackDuration = 2f; // Время, за которое камера плавно возвращается к оригинальному размеру

    private Camera mainCamera;
    private float originalSize; // Исходный размер камеры
    private bool isZooming = false; // Флаг для проверки, что зум активен

    void Start() {
        // Получаем ссылку на камеру и сохраняем ее исходный размер
        mainCamera = Camera.main;
        originalSize = mainCamera.orthographicSize;

        // Запускаем зумирование камеры сразу при старте
        ZoomIn();
    }

    public void ZoomIn() {
        if (!isZooming) {
            StartCoroutine(ZoomEffect(zoomedSize, zoomDuration, false)); // Плавный зум на целевой размер
        }
    }

    private IEnumerator ZoomEffect(float targetSize, float duration, bool isReturning) {
        isZooming = true;

        // Отключаем скрипт CameraFollow, чтобы не мешал зумированию
        if (!isReturning && cameraFollow != null) {
            cameraFollow.enabled = false;
        }

        float timeElapsed = 0f;
        float startSize = mainCamera.orthographicSize;

        // Плавный переход от текущего размера к целевому
        while (timeElapsed < duration) {
            mainCamera.orthographicSize = Mathf.Lerp(startSize, targetSize, timeElapsed / duration);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Убедимся, что размер точно стал целевым
        mainCamera.orthographicSize = targetSize;

        if (!isReturning) {
            // После завершения зума ждём несколько секунд, затем возвращаемся обратно
            yield return new WaitForSeconds(zoomDuration); // Задержка перед возвратом
            StartCoroutine(ZoomEffect(originalSize, zoomBackDuration, true)); // Плавный возврат к исходному размеру
        } else {
            // После возврата включаем CameraFollow
            if (cameraFollow != null) {
                cameraFollow.enabled = true;
            }
            isZooming = false;
        }
    }
}

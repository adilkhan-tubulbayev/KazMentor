using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Добавляем пространство имён для работы со сценами

public class Player : MonoBehaviour {
    public static Player Instance { get; private set; }

    [SerializeField] private float movingSpeed = 10f;

    private Rigidbody2D rb;

    private float minMovingSpeed = 0.1f;
    private bool isRunning = false;

    public bool isDialogueActive = false; // Новый флаг для блокировки движения

    private bool isPlayingSound = false; // Флаг для отслеживания состояния звука

    private void Awake() {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if (!isDialogueActive) { // Проверка флага
            HandleMovement();
        } else {
            rb.velocity = Vector2.zero; // Останавливаем движение
            isRunning = false;
            StopWalkingSound(); // Останавливаем звук при остановке
        }
    }

    private void HandleMovement() {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();

        // Нормализация вектора для единообразного движения
        inputVector = inputVector.normalized;

        // Перемещение Rigidbody2D
        rb.MovePosition(rb.position + inputVector * (movingSpeed * Time.fixedDeltaTime));

        if (Mathf.Abs(inputVector.x) > minMovingSpeed || Mathf.Abs(inputVector.y) > minMovingSpeed) {
            isRunning = true;
            PlayWalkingSound();
        } else {
            isRunning = false;
            StopWalkingSound();
        }
    }

    private void PlayWalkingSound() {
        if (!isPlayingSound) { // Проверяем, не воспроизводится ли уже звук
            isPlayingSound = true;

            // Получаем активную сцену
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            // В зависимости от имени сцены воспроизводим соответствующий звук
            if (sceneName == "SchoolOutside" || sceneName == "Electronium") {
                AudioManager.Instance.PlayGroundWalkingSound();
            } else if (sceneName == "SchoolLobby" || sceneName == "Physics") {
                AudioManager.Instance.PlayFloorWalkingSound();
            }
        }
    }

    private void StopWalkingSound() {
        if (isPlayingSound) {
            isPlayingSound = false;

            // Останавливаем оба звука, чтобы избежать их наложения
            AudioManager.Instance.StopAudioClip(AudioManager.Instance.floorWalking);
            AudioManager.Instance.StopAudioClip(AudioManager.Instance.groundWalking);
        }
    }

    public bool IsRunning() {
        return isRunning;
    }

    public Vector3 GetPlayerPosition() {
        Vector3 playerScreenPosition = Camera.main.WorldToScreenPoint(transform.position);
        return playerScreenPosition;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Coin")) {
            Destroy(other.gameObject);
            if (CoinManager.Instance != null) {
                CoinManager.Instance.AddCoins(1); // Добавляем коины через метод AddCoins
            }
        }
    }

    // Метод вызывается при загрузке новой сцены
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (CoinManager.Instance != null) {
            CoinManager.Instance.ResetSceneCoins(); // Сбрасываем монеты текущей сцены
            CoinTextManager.Instance.UpdateCoinText(CoinManager.totalCoins); // Обновляем текст
        }
    }

    // Подписываемся на событие загрузки сцены при активации объекта
    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Отписываемся от события загрузки сцены при деактивации объекта
    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ��������� ������������ ��� ��� ������ �� �������

public class Player : MonoBehaviour {
    public static Player Instance { get; private set; }

    [SerializeField] private float movingSpeed = 10f;

    private Rigidbody2D rb;

    private float minMovingSpeed = 0.1f;
    private bool isRunning = false;

    public bool isDialogueActive = false; // ����� ���� ��� ���������� ��������

    private bool isPlayingSound = false; // ���� ��� ������������ ��������� �����

    private void Awake() {
        Instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate() {
        if (!isDialogueActive) { // �������� �����
            HandleMovement();
        } else {
            rb.velocity = Vector2.zero; // ������������� ��������
            isRunning = false;
            StopWalkingSound(); // ������������� ���� ��� ���������
        }
    }

    private void HandleMovement() {
        Vector2 inputVector = GameInput.Instance.GetMovementVector();

        // ������������ ������� ��� �������������� ��������
        inputVector = inputVector.normalized;

        // ����������� Rigidbody2D
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
        if (!isPlayingSound) { // ���������, �� ��������������� �� ��� ����
            isPlayingSound = true;

            // �������� �������� �����
            Scene currentScene = SceneManager.GetActiveScene();
            string sceneName = currentScene.name;

            // � ����������� �� ����� ����� ������������� ��������������� ����
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

            // ������������� ��� �����, ����� �������� �� ���������
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
                CoinManager.Instance.AddCoins(1); // ��������� ����� ����� ����� AddCoins
            }
        }
    }

    // ����� ���������� ��� �������� ����� �����
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (CoinManager.Instance != null) {
            CoinManager.Instance.ResetSceneCoins(); // ���������� ������ ������� �����
            CoinTextManager.Instance.UpdateCoinText(CoinManager.totalCoins); // ��������� �����
        }
    }

    // ������������� �� ������� �������� ����� ��� ��������� �������
    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // ������������ �� ������� �������� ����� ��� ����������� �������
    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}

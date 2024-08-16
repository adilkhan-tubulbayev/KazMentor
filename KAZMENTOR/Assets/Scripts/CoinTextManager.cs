using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinTextManager : MonoBehaviour {
    public static CoinTextManager Instance { get; private set; }
    public TextMeshProUGUI coinText;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ������� ��� ����� ����
        } else {
            Destroy(gameObject); // ������� ������������� ���������
        }
    }

    private void Start() {
        if (coinText == null) {
            FindAndAssignCoinText();
        }

        // �������� ����� ��� ������
        if (CoinManager.Instance != null) {
            UpdateCoinText(CoinManager.totalCoins);
        }
    }

    private void OnEnable() {
        // ������������� �� ������� �������� �����
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        // ������������ �� ������� �������� �����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        // ������������� CoinText ����� �������� ����� �����
        FindAndAssignCoinText();
        if (CoinManager.Instance != null) {
            UpdateCoinText(CoinManager.totalCoins);
        }
    }

    private void FindAndAssignCoinText() {
        GameObject coinTextObject = GameObject.FindGameObjectWithTag("CoinText");
        if (coinTextObject != null) {
            coinText = coinTextObject.GetComponent<TextMeshProUGUI>();
        } else {
            Debug.LogError("�� ������� ����� ������ � ����� CoinText � �����!");
        }
    }

    public void UpdateCoinText(int coinCount) {
        if (coinText != null) {
            coinText.text = coinCount.ToString();
        } else {
            Debug.LogError("CoinText �� �������� � CoinTextManager!");
        }
    }
}

using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class CoinTextManager : MonoBehaviour {
    public static CoinTextManager Instance { get; private set; }
    public TextMeshProUGUI coinText;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Не удалять при смене сцен
        } else {
            Destroy(gameObject); // Удалить дублирующийся экземпляр
        }
    }

    private void Start() {
        if (coinText == null) {
            FindAndAssignCoinText();
        }

        // Обновить текст при старте
        if (CoinManager.Instance != null) {
            UpdateCoinText(CoinManager.totalCoins);
        }
    }

    private void OnEnable() {
        // Подписываемся на события загрузки сцены
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        // Отписываемся от событий загрузки сцены
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode) {
        // Переназначаем CoinText после загрузки новой сцены
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
            Debug.LogError("Не удалось найти объект с тегом CoinText в сцене!");
        }
    }

    public void UpdateCoinText(int coinCount) {
        if (coinText != null) {
            coinText.text = coinCount.ToString();
        } else {
            Debug.LogError("CoinText не назначен в CoinTextManager!");
        }
    }
}

using UnityEngine;
using TMPro;

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
        // Найти и присвоить компонент TextMeshProUGUI, если он не назначен
        if (coinText == null) {
            FindAndAssignCoinText();
        }

        // Обновить текст при старте
        if (CoinManager.Instance != null) {
            UpdateCoinText(CoinManager.Instance.coinCount);
        }
    }

    private void FindAndAssignCoinText() {
        Debug.Log("Поиск объекта с тегом CoinText...");
        GameObject coinTextObject = GameObject.FindGameObjectWithTag("CoinText");
        if (coinTextObject != null) {
            Debug.Log("Объект с тегом CoinText найден.");
            coinText = coinTextObject.GetComponent<TextMeshProUGUI>();
            if (coinText != null) {
                Debug.Log("Компонент TextMeshProUGUI найден и назначен.");
            } else {
                Debug.LogError("Компонент TextMeshProUGUI не найден на объекте с тегом CoinText!");
            }
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

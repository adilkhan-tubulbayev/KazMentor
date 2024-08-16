using UnityEngine;

public class CoinManager : MonoBehaviour {
    public static CoinManager Instance { get; private set; }
    private int currentSceneCoins = 0; // Монеты, собранные на текущей сцене

    public static int totalCoins = 0; // Общий счет монет, который сохраняется между сценами

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Не удалять при смене сцен
        } else {
            Destroy(gameObject); // Удалить дублирующийся экземпляр
        }
    }

    public void AddCoins(int amount) {
        currentSceneCoins += amount;
        totalCoins += amount;

        // Воспроизводим звук сбора монеты
        AudioManager.Instance.PlayCoinSound();

        // Обновляем текст с количеством монет
        if (CoinTextManager.Instance != null) {
            CoinTextManager.Instance.UpdateCoinText(totalCoins);
        }
    }

    public void ResetSceneCoins() {
        currentSceneCoins = 0;
    }

    public void SaveSceneCoins() {
        totalCoins += currentSceneCoins;
    }
}

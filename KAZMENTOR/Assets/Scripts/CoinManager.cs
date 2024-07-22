using UnityEngine;

public class CoinManager : MonoBehaviour {
    public static CoinManager Instance { get; private set; }
    public int coinCount;

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ������� ��� ����� ����
        } else {
            Destroy(gameObject); // ������� ������������� ���������
        }
    }

    public void AddCoins(int amount) {
        coinCount += amount;
        if (CoinTextManager.Instance != null) {
            CoinTextManager.Instance.UpdateCoinText(coinCount);
        }
    }
}

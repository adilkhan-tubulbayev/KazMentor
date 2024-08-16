using UnityEngine;

public class CoinManager : MonoBehaviour {
    public static CoinManager Instance { get; private set; }
    private int currentSceneCoins = 0; // ������, ��������� �� ������� �����

    public static int totalCoins = 0; // ����� ���� �����, ������� ����������� ����� �������

    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ������� ��� ����� ����
        } else {
            Destroy(gameObject); // ������� ������������� ���������
        }
    }

    public void AddCoins(int amount) {
        currentSceneCoins += amount;
        totalCoins += amount;

        // ������������� ���� ����� ������
        AudioManager.Instance.PlayCoinSound();

        // ��������� ����� � ����������� �����
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

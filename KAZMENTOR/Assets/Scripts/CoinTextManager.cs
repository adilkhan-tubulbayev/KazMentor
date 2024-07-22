using UnityEngine;
using TMPro;

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
        // ����� � ��������� ��������� TextMeshProUGUI, ���� �� �� ��������
        if (coinText == null) {
            FindAndAssignCoinText();
        }

        // �������� ����� ��� ������
        if (CoinManager.Instance != null) {
            UpdateCoinText(CoinManager.Instance.coinCount);
        }
    }

    private void FindAndAssignCoinText() {
        Debug.Log("����� ������� � ����� CoinText...");
        GameObject coinTextObject = GameObject.FindGameObjectWithTag("CoinText");
        if (coinTextObject != null) {
            Debug.Log("������ � ����� CoinText ������.");
            coinText = coinTextObject.GetComponent<TextMeshProUGUI>();
            if (coinText != null) {
                Debug.Log("��������� TextMeshProUGUI ������ � ��������.");
            } else {
                Debug.LogError("��������� TextMeshProUGUI �� ������ �� ������� � ����� CoinText!");
            }
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

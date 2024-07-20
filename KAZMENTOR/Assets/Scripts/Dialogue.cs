using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour {
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public GameObject player; // Ссылка на объект игрока

    private int index;
    private Player playerScript; // Ссылка на скрипт управления персонажем

    // Start is called before the first frame update
    void Start() {
        textComponent.text = string.Empty;

        if (player != null) {
            playerScript = player.GetComponent<Player>();
        } else {
            Debug.LogError("Player is not assigned in the inspector!");
        }
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (textComponent.text == lines[index]) {
                NextLine();
            } else {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }
    }

    public void StartDialogue() {
        index = 0;
        StartCoroutine(TypeLine());
        if (playerScript != null) {
            playerScript.isDialogueActive = true; // Заблокировать движение персонажа
        }
    }

    IEnumerator TypeLine() {
        textComponent.text = string.Empty; // Очистка текста перед началом новой линии
        foreach (char c in lines[index].ToCharArray()) {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }

    void NextLine() {
        if (index < lines.Length - 1) {
            index++;
            StartCoroutine(TypeLine());
        } else {
            gameObject.SetActive(false);
            if (playerScript != null) {
                playerScript.isDialogueActive = false; // Разблокировать движение персонажа
            }
        }
    }

    public void ResetDialogue() {
        index = 0; // Сброс индекса на начало
        StartDialogue(); // Перезапуск диалога
    }

    public void ExitDialogue() {
        gameObject.SetActive(false); // Выключить диалоговое окно
        if (playerScript != null) {
            playerScript.isDialogueActive = false; // Разблокировать движение персонажа
        }
    }
}

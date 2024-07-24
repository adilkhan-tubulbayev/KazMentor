using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour {
    public Animator barrierAnimator; // Ссылка на аниматор барьера
    public GameObject[] canvases; // Массив канвасов, которые нужно отключить
    public GameObject Player; // Ссылка на объект игрока
    private Player playerScript; // Ссылка на скрипт игрока

    private bool isCircuitCorrect = false;
    private bool isTextCorrect = false;

    private void Start() {
        if (Player != null) {
            playerScript = Player.GetComponent<Player>();
        } else {
            Debug.LogError("Player is not assigned in the inspector!");
        }
    }

    public void SetCircuitCorrect(bool value) {
        isCircuitCorrect = value;
        CheckAllConditions();
    }

    public void SetTextCorrect(bool value) {
        isTextCorrect = value;
        CheckAllConditions();
    }

    private void CheckAllConditions() {
        if (isCircuitCorrect && isTextCorrect) {
            if (barrierAnimator != null && barrierAnimator.HasParameter("BarrierOff")) {
                barrierAnimator.SetTrigger("BarrierOff"); // Запуск анимации BarrierOff
            } else {
                Debug.LogError("Аниматор не настроен правильно или параметр BarrierOff отсутствует.");
            }

            // Проверка, что playerScript существует перед доступом к нему
            if (playerScript != null) {
                playerScript.isDialogueActive = false; // Разблокируем движение персонажа
            }

            // Закрытие всех канвасов
            StartCoroutine(CloseCanvasesAfterAnimation());
        }
    }

    private IEnumerator CloseCanvasesAfterAnimation() {
        yield return new WaitForSeconds(1); // Ждем завершения анимации, указываем точное время анимации

        foreach (var canvas in canvases) {
            canvas.SetActive(false); // Отключаем все канвасы
        }
    }
}

// Вспомогательный метод для проверки параметра
public static class AnimatorExtensions {
    public static bool HasParameter(this Animator animator, string paramName) {
        foreach (AnimatorControllerParameter param in animator.parameters) {
            if (param.name == paramName) {
                return true;
            }
        }
        return false;
    }
}

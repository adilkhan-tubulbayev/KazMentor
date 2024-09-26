using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class TooltipManager : MonoBehaviour {
    public static TooltipManager Instance;

    public CanvasGroup tooltipCanvasGroup;
    public TextMeshProUGUI tooltipText;
    public float fadeDuration = 0.5f;

    private Coroutine fadeCoroutine;

    void Awake() {
        if (Instance == null) {
            Instance = this;
            // Если хотите, чтобы менеджер сохранялся между сценами, раскомментируйте следующую строку
            // DontDestroyOnLoad(this.gameObject);
        } else {
            Destroy(gameObject);
        }

        // Убедитесь, что подсказка невидима при старте
        tooltipCanvasGroup.alpha = 0f;
    }

    public void ShowTooltip(string text) {
        tooltipText.text = text;

        if (fadeCoroutine != null) {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeTooltip(1f));
    }

    public void HideTooltip() {
        if (fadeCoroutine != null) {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeTooltip(0f));
    }

    IEnumerator FadeTooltip(float targetAlpha) {
        float startAlpha = tooltipCanvasGroup.alpha;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration) {
            tooltipCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        tooltipCanvasGroup.alpha = targetAlpha;
    }
}

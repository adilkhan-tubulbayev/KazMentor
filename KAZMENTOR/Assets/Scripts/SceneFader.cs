using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour {
    public Image fadeImage;
    public float fadeSpeed = 1.5f;

    void Start() {
        StartCoroutine(FadeOut());
        fadeImage.raycastTarget = false;
    }

    public void FadeToScene(string sceneName) {
        StartCoroutine(FadeIn(sceneName));
    }

    IEnumerator FadeIn(string sceneName) {
        float progress = 0.0f;
        while (progress < 1.0f) {
            fadeImage.color = new Color(0, 0, 0, progress);
            progress += fadeSpeed * Time.deltaTime;
            yield return null;
        }
        SceneManager.LoadScene(sceneName);
        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut() {
        float progress = 1.0f;
        while (progress > 0.0f) {
            fadeImage.color = new Color(0, 0, 0, progress);
            progress -= fadeSpeed * Time.deltaTime;
            yield return null;
        }
    }


}
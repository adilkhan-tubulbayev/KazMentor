using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour {
    public Image fadeImage;
    public float fadeSpeed = 1.5f;

    private static SceneFader instance;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
            return;
        }
    }

    void Start() {
        fadeImage.raycastTarget = false;
        StartCoroutine(FadeOut());
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

        // Ensure the fadeImage persists
        DontDestroyOnLoad(fadeImage.transform.parent.gameObject);

        SceneManager.LoadScene(sceneName);

        // Wait for the scene to load before starting FadeOut
        yield return new WaitForSeconds(0.1f);

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

using UnityEngine;
using UnityEngine.SceneManagement;

public class PlanetInteraction : MonoBehaviour {

    private Vector3 originalScale;
    [SerializeField] float planetScale = 1.1f;
    [SerializeField] string planetTransition = "Electronium";

    private void Start() {
        originalScale = transform.localScale;
    }

    private void OnMouseEnter() {
        transform.localScale = originalScale * planetScale;
    }

    private void OnMouseExit() {
        transform.localScale = originalScale;
    }

    private void OnMouseDown() {
        SceneManager.LoadScene(planetTransition);
    }
}

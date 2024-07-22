using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonScale : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
    private Vector3 originalScale;
    [SerializeField] private float buttonScale = 1.1f;

    private void Start() {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData) {
        transform.localScale = originalScale * buttonScale;
    }

    public void OnPointerExit(PointerEventData eventData) {
        transform.localScale = originalScale;
    }
}

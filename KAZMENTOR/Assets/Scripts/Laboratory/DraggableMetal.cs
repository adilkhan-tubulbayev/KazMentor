using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableMetal : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private Canvas canvas;                // Reference to the Canvas
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;     // Original position of the metal
    private Transform originalParent;     // Original parent of the metal

    [HideInInspector]
    public Scale currentScale;            // Reference to the Scale if the metal is on it

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();   // Get the RectTransform
        canvasGroup = GetComponent<CanvasGroup>();       // Get the CanvasGroup
        canvas = GetComponentInParent<Canvas>();         // Get the Canvas

        originalPosition = rectTransform.anchoredPosition;   // Store the original position
        originalParent = transform.parent;                   // Store the original parent
    }

    // Called when the drag operation starts
    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = false;  // Disable raycasts so that drop zones can detect the metal

        if (currentScale != null) {
            // Inform the scale that the metal is being removed
            currentScale.ClearMass();
            currentScale = null;
        }
    }

    // Called during the drag operation
    public void OnDrag(PointerEventData eventData) {
        // Move the metal object with the mouse
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out localPoint);

        rectTransform.localPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = true;  // �������� ����� raycasts

        // ���������, ��� �� ������ ������� �� ����
        if (eventData.pointerEnter != null) {
            Scale scale = eventData.pointerEnter.GetComponent<Scale>();
            if (scale != null) {
                // ������������� ������� ������� ����� ��� ������
                rectTransform.anchoredPosition = scale.GetComponent<RectTransform>().anchoredPosition + new Vector2(0, 145); // ������� ����� �� 145 ������

                currentScale = scale;  // ������������� ������ �� ������� ����
                scale.DisplayMass(this);  // ���������� �����
                return;
            }
        }

        // ���� ������ �� ������� �� ����, ��������� ��� � ������� �������
    }



}

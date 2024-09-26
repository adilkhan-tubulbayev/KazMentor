using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableMetal : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private Canvas canvas;                // ������ �� Canvas ��� UI
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;     // ������������ ������� �������
    private Transform originalParent;     // ������������ �������� �������

    [HideInInspector]
    public Scale currentScale;            // ������ �� ������ �����, �� ������� ��������� ������

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();   // �������� RectTransform
        canvasGroup = GetComponent<CanvasGroup>();       // �������� CanvasGroup
        canvas = GetComponentInParent<Canvas>();         // �������� Canvas

        originalPosition = rectTransform.anchoredPosition;   // ���������� ������������ �������
        originalParent = transform.parent;                   // ���������� ������������� ��������
    }

    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = false;  // ��������� �������������� � raycasts, ���� ������ ���������������

        if (currentScale != null) {
            // ���� ������ ��� �� �����, ���������� ����� � ����������� �� �����
            currentScale.ClearMass();
            currentScale = null;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        // ���������� ������ ������ � �����
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out localPoint);

        rectTransform.localPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = true;  // �������� ������� raycasts

        // ���������, ��� �� ������ ������� �� ����
        if (eventData.pointerEnter != null) {
            Scale scale = eventData.pointerEnter.GetComponent<Scale>();
            if (scale != null) {
                // ��������� ������ �� �����
                rectTransform.anchoredPosition = scale.GetComponent<RectTransform>().anchoredPosition + new Vector2(0, 145); // ������� �����

                currentScale = scale;  // ����������� ������ � �����
                scale.DisplayMass(this);  // ���������� �����
                return;
            }
        }

        // ���� ������ �� ��� ������� �� ����, ���������� ��� � ������� �������
    }
}

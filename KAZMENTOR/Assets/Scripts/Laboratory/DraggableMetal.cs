using UnityEngine;
using UnityEngine.EventSystems;

public class DraggableMetal : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private Canvas canvas;                // Ссылка на Canvas для UI
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Vector2 originalPosition;     // Оригинальная позиция металла
    private Transform originalParent;     // Оригинальный родитель объекта

    [HideInInspector]
    public Scale currentScale;            // Ссылка на объект весов, на которых находится металл

    private void Awake() {
        rectTransform = GetComponent<RectTransform>();   // Получаем RectTransform
        canvasGroup = GetComponent<CanvasGroup>();       // Получаем CanvasGroup
        canvas = GetComponentInParent<Canvas>();         // Получаем Canvas

        originalPosition = rectTransform.anchoredPosition;   // Запоминаем оригинальную позицию
        originalParent = transform.parent;                   // Запоминаем оригинального родителя
    }

    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = false;  // Отключаем взаимодействие с raycasts, пока объект перетаскивается

        if (currentScale != null) {
            // Если металл был на весах, сбрасываем массу и отсоединяем от весов
            currentScale.ClearMass();
            currentScale = null;
        }
    }

    public void OnDrag(PointerEventData eventData) {
        // Перемещаем объект вместе с мышью
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            eventData.position,
            canvas.worldCamera,
            out localPoint);

        rectTransform.localPosition = localPoint;
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = true;  // Включаем обратно raycasts

        // Проверяем, был ли металл сброшен на весы
        if (eventData.pointerEnter != null) {
            Scale scale = eventData.pointerEnter.GetComponent<Scale>();
            if (scale != null) {
                // Фиксируем металл на весах
                rectTransform.anchoredPosition = scale.GetComponent<RectTransform>().anchoredPosition + new Vector2(0, 145); // Смещаем вверх

                currentScale = scale;  // Привязываем металл к весам
                scale.DisplayMass(this);  // Отображаем массу
                return;
            }
        }

        // Если объект не был сброшен на весы, возвращаем его в текущую позицию
    }
}

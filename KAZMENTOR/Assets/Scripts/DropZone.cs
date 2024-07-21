using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler {
    public void OnDrop(PointerEventData eventData) {
        DraggableItem draggable = eventData.pointerDrag.GetComponent<DraggableItem>();
        if (draggable != null) {
            draggable.transform.SetParent(transform); // ѕереместить объект в родительский объект зоны сброса
            draggable.GetComponent<RectTransform>().anchoredPosition = Vector2.zero; // —бросить позицию
        }
    }
}

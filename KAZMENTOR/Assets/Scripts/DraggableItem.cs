using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Transform originalParent; // Делаем переменную публичной, чтобы CircuitChecker мог получить доступ

    private Vector3 startPosition;
    private CanvasGroup canvasGroup;
    private DropZone currentDropZone;

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        GetComponent<Rigidbody2D>().isKinematic = true; // Установка Rigidbody2D в Kinematic
        startPosition = transform.position;
        originalParent = transform.parent; // Сохраняем исходного родителя
    }

    public void OnBeginDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = false;
        Debug.Log("Начало перетаскивания: " + gameObject.name);
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
        Debug.Log("Перетаскивание: " + gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = true;
        Debug.Log("Конец перетаскивания: " + gameObject.name);

        if (currentDropZone != null) {
            transform.SetParent(currentDropZone.transform);
            transform.position = currentDropZone.transform.position;
            Debug.Log($"{gameObject.name} сброшен в зону {currentDropZone.name}");
        } else {
            transform.position = startPosition;
            transform.SetParent(originalParent);
            Debug.Log($"{gameObject.name} возвращён на исходную позицию");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        DropZone dropZone = collision.GetComponent<DropZone>();
        if (dropZone != null) {
            currentDropZone = dropZone;
            dropZone.SetCurrentItem(this);
            Debug.Log($"{gameObject.name} вошёл в зону {dropZone.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        DropZone dropZone = collision.GetComponent<DropZone>();
        if (dropZone != null && dropZone == currentDropZone) {
            dropZone.ClearCurrentItem();
            currentDropZone = null;
            Debug.Log($"{gameObject.name} покинул зону {dropZone.name}");
        }
    }

    public void SetDroppedZone(DropZone dropZone) {
        currentDropZone = dropZone;
    }

    public void ClearDroppedZone() {
        currentDropZone = null;
    }
}

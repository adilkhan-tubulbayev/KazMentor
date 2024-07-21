using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private Vector3 startPosition;
    private Transform originalParent;
    private CanvasGroup canvasGroup;
    private DropZone currentDropZone;

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        GetComponent<Rigidbody2D>().isKinematic = true; // Установка Rigidbody2D в Kinematic
    }

    public void OnBeginDrag(PointerEventData eventData) {
        startPosition = transform.position;
        originalParent = transform.parent;
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
            if (currentDropZone.HasCorrectItem(this)) {
                transform.SetParent(currentDropZone.transform);
                transform.position = currentDropZone.transform.position;
                Debug.Log($"{gameObject.name} сброшен в правильную зону {currentDropZone.name}");
            } else {
                transform.position = startPosition;
                transform.SetParent(originalParent);
                Debug.Log($"{gameObject.name} возвращён на исходную позицию (неправильная зона)");
            }
        } else {
            transform.position = startPosition;
            transform.SetParent(originalParent);
            Debug.Log($"{gameObject.name} возвращён на исходную позицию (нет зоны)");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        DropZone dropZone = collision.GetComponent<DropZone>();
        if (dropZone != null) {
            currentDropZone = dropZone;
            Debug.Log($"{gameObject.name} вошёл в зону {dropZone.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        DropZone dropZone = collision.GetComponent<DropZone>();
        if (dropZone != null && dropZone == currentDropZone) {
            currentDropZone = null;
            Debug.Log($"{gameObject.name} покинул зону {dropZone.name}");
        }
    }
}

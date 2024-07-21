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
        GetComponent<Rigidbody2D>().isKinematic = true; // ��������� Rigidbody2D � Kinematic
    }

    public void OnBeginDrag(PointerEventData eventData) {
        startPosition = transform.position;
        originalParent = transform.parent;
        canvasGroup.blocksRaycasts = false;
        Debug.Log("������ ��������������: " + gameObject.name);
    }

    public void OnDrag(PointerEventData eventData) {
        transform.position = Input.mousePosition;
        Debug.Log("��������������: " + gameObject.name);
    }

    public void OnEndDrag(PointerEventData eventData) {
        canvasGroup.blocksRaycasts = true;
        Debug.Log("����� ��������������: " + gameObject.name);

        if (currentDropZone != null) {
            if (currentDropZone.HasCorrectItem(this)) {
                transform.SetParent(currentDropZone.transform);
                transform.position = currentDropZone.transform.position;
                Debug.Log($"{gameObject.name} ������� � ���������� ���� {currentDropZone.name}");
            } else {
                transform.position = startPosition;
                transform.SetParent(originalParent);
                Debug.Log($"{gameObject.name} ��������� �� �������� ������� (������������ ����)");
            }
        } else {
            transform.position = startPosition;
            transform.SetParent(originalParent);
            Debug.Log($"{gameObject.name} ��������� �� �������� ������� (��� ����)");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        DropZone dropZone = collision.GetComponent<DropZone>();
        if (dropZone != null) {
            currentDropZone = dropZone;
            Debug.Log($"{gameObject.name} ����� � ���� {dropZone.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        DropZone dropZone = collision.GetComponent<DropZone>();
        if (dropZone != null && dropZone == currentDropZone) {
            currentDropZone = null;
            Debug.Log($"{gameObject.name} ������� ���� {dropZone.name}");
        }
    }
}

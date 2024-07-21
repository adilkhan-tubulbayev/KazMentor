using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    public Transform originalParent; // ������ ���������� ���������, ����� CircuitChecker ��� �������� ������

    private Vector3 startPosition;
    private CanvasGroup canvasGroup;
    private DropZone currentDropZone;

    private void Awake() {
        canvasGroup = GetComponent<CanvasGroup>();
        GetComponent<Rigidbody2D>().isKinematic = true; // ��������� Rigidbody2D � Kinematic
        startPosition = transform.position;
        originalParent = transform.parent; // ��������� ��������� ��������
    }

    public void OnBeginDrag(PointerEventData eventData) {
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
            transform.SetParent(currentDropZone.transform);
            transform.position = currentDropZone.transform.position;
            Debug.Log($"{gameObject.name} ������� � ���� {currentDropZone.name}");
        } else {
            transform.position = startPosition;
            transform.SetParent(originalParent);
            Debug.Log($"{gameObject.name} ��������� �� �������� �������");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        DropZone dropZone = collision.GetComponent<DropZone>();
        if (dropZone != null) {
            currentDropZone = dropZone;
            dropZone.SetCurrentItem(this);
            Debug.Log($"{gameObject.name} ����� � ���� {dropZone.name}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        DropZone dropZone = collision.GetComponent<DropZone>();
        if (dropZone != null && dropZone == currentDropZone) {
            dropZone.ClearCurrentItem();
            currentDropZone = null;
            Debug.Log($"{gameObject.name} ������� ���� {dropZone.name}");
        }
    }

    public void SetDroppedZone(DropZone dropZone) {
        currentDropZone = dropZone;
    }

    public void ClearDroppedZone() {
        currentDropZone = null;
    }
}

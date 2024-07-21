using UnityEngine;

public class DropZone : MonoBehaviour {
    private DraggableItem currentItem;

    private void OnTriggerEnter2D(Collider2D collision) {
        DraggableItem item = collision.GetComponent<DraggableItem>();
        if (item != null) {
            currentItem = item;
            Debug.Log($"������ {item.name} ����� � ���� {name}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        DraggableItem item = collision.GetComponent<DraggableItem>();
        if (item != null && item == currentItem) {
            currentItem = null;
            Debug.Log($"������ {item.name} ������� ���� {name}");
        }
    }

    public bool HasCorrectItem(string correctTag) {
        bool isCorrectItem = currentItem != null && currentItem.CompareTag(correctTag);
        Debug.Log($"�������� ���� {name}: {(isCorrectItem ? "�����" : "�������")}");
        return isCorrectItem;
    }

    public void SetCurrentItem(DraggableItem item) {
        currentItem = item;
    }

    public void ClearCurrentItem() {
        currentItem = null;
    }
}

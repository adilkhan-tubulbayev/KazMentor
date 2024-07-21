using UnityEngine;

public class DropZone : MonoBehaviour {
    public string correctTag; // Tag that indicates the correct item for this zone
    private DraggableItem currentItem;

    private void OnTriggerEnter2D(Collider2D collision) {
        DraggableItem item = collision.GetComponent<DraggableItem>();
        if (item != null && item.CompareTag(correctTag)) {
            currentItem = item;
            Debug.Log($"Объект {item.name} вошёл в правильную зону {name}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        DraggableItem item = collision.GetComponent<DraggableItem>();
        if (item != null && item == currentItem) {
            currentItem = null;
            Debug.Log($"Объект {item.name} покинул зону {name}");
        }
    }

    public bool HasCorrectItem(DraggableItem item) {
        bool isCorrectItem = currentItem == item && item.CompareTag(correctTag);
        Debug.Log($"Проверка {item.name} в зоне {name}: {(isCorrectItem ? "верно" : "неверно")}");
        return isCorrectItem;
    }
}

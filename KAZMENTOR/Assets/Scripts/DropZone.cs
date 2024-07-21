using UnityEngine;

public class DropZone : MonoBehaviour {
    private DraggableItem currentItem;

    private void OnTriggerEnter2D(Collider2D collision) {
        DraggableItem item = collision.GetComponent<DraggableItem>();
        if (item != null) {
            currentItem = item;
            Debug.Log($"Объект {item.name} вошёл в зону {name}");
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        DraggableItem item = collision.GetComponent<DraggableItem>();
        if (item != null && item == currentItem) {
            currentItem = null;
            Debug.Log($"Объект {item.name} покинул зону {name}");
        }
    }

    public bool HasCorrectItem(string correctTag) {
        bool isCorrectItem = currentItem != null && currentItem.CompareTag(correctTag);
        Debug.Log($"Проверка зоны {name}: {(isCorrectItem ? "верно" : "неверно")}");
        return isCorrectItem;
    }

    public void SetCurrentItem(DraggableItem item) {
        currentItem = item;
    }

    public void ClearCurrentItem() {
        currentItem = null;
    }
}

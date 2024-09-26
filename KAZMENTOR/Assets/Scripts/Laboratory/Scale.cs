using UnityEngine;
using TMPro;                      // Для работы с TextMeshPro
using UnityEngine.EventSystems;

public class Scale : MonoBehaviour, IDropHandler {
    public TextMeshProUGUI massText;       // Текст для отображения массы

    private DraggableMetal currentMetal;  // Металл, который находится на весах

    // Метод для отображения массы
    public void DisplayMass(DraggableMetal metal) {
        currentMetal = metal;

        // Проверяем массу металла, используя его скрипт свойств (MetalProperties)
        MetalProperties metalProperties = metal.GetComponent<MetalProperties>();

        if (metalProperties != null) {
            massText.text = "Mass: " + metalProperties.mass.ToString("F2") + " kg";
        } else {
            massText.text = "Mass: unknown";
        }
    }

    // Метод для сброса массы (обнуление)
    public void ClearMass() {
        massText.text = "Mass: 0 kg";
        currentMetal = null;
    }

    // Обрабатываем сброс объекта на весы
    public void OnDrop(PointerEventData eventData) {
        DraggableMetal metal = eventData.pointerDrag.GetComponent<DraggableMetal>();
        if (metal != null) {
            // Фиксируем металл на весах
            metal.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition + new Vector2(0, 50); // Смещаем вверх

            metal.currentScale = this;  // Привязываем весы к металлу
            DisplayMass(metal);  // Отображаем массу
        }
    }
}

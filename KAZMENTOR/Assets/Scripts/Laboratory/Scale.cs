using UnityEngine;
using TMPro;                      // For TextMeshProUGUI
using UnityEngine.EventSystems;

public class Scale : MonoBehaviour, IDropHandler {
    public TextMeshProUGUI massText;       // Text to display the mass

    private DraggableMetal currentMetal;  // The metal currently on the scale

    // Method to display the mass
    public void DisplayMass(DraggableMetal metal) {
        currentMetal = metal;

        float mass = 0f;

        // Attempt to get the mass from the metal's properties
        // Assuming you have scripts like CopperProperties, AluminumProperties, etc.
        // attached to your metal objects that contain a 'mass' variable

        MonoBehaviour[] metalProperties = metal.GetComponents<MonoBehaviour>();

        foreach (MonoBehaviour prop in metalProperties) {
            if (prop is CopperProperties copper) {
                mass = copper.mass;
                break;
            } else if (prop is AluminumProperties aluminum) {
                mass = aluminum.mass;
                break;
            } else if (prop is PlumbumProperties plumbum) {
                mass = plumbum.mass;
                break;
            }
        }

        if (mass > 0f) {
            massText.text = "Mass: " + mass.ToString("F2") + " kg";
        } else {
            massText.text = "Mass: unknown";
        }
    }

    // Method to clear the mass display (reset to zero)
    public void ClearMass() {
        massText.text = "Mass: 0 kg";
        currentMetal = null;
    }

    // Called when an object is dropped onto the scale
    public void OnDrop(PointerEventData eventData) {
        DraggableMetal metal = eventData.pointerDrag.GetComponent<DraggableMetal>();
        if (metal != null) {
            // Фиксируем позицию металла точно над весами
            metal.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition + new Vector2(0, 50); // Смещаем вверх на 50 единиц

            // Устанавливаем текущие весы для металла
            metal.currentScale = this;
            DisplayMass(metal);
        }
    }



}

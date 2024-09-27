using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Scale : MonoBehaviour, IDropHandler {
    public TextMeshProUGUI massText;

    private DraggableMetal currentMetal;

    public void DisplayMass(DraggableMetal metal) {
        currentMetal = metal;

        MetalProperties metalProperties = metal.GetComponent<MetalProperties>();

        if (metalProperties != null) {
            massText.text = "Mass: " + metalProperties.mass.ToString("F2") + " kg";
        } else {
            massText.text = "Mass: unknown";
        }
    }

    public void ClearMass() {
        massText.text = "Mass: 0 kg";
        currentMetal = null;
    }

    public void OnDrop(PointerEventData eventData) {
        DraggableMetal metal = eventData.pointerDrag.GetComponent<DraggableMetal>();
        if (metal != null) {
            metal.GetComponent<RectTransform>().anchoredPosition = GetComponent<RectTransform>().anchoredPosition + new Vector2(0, 50);

            metal.currentScale = this;
            DisplayMass(metal);

            AudioManager.Instance.PlayLaboratoryInterfaceSound(); // Воспроизведение звука
        }
    }
}

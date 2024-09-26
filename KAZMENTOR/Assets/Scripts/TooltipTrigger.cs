using UnityEngine;

public class TooltipTrigger : MonoBehaviour {
    [TextArea]
    public string tooltipText;

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            TooltipManager.Instance.ShowTooltip(tooltipText);
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            TooltipManager.Instance.HideTooltip();
        }
    }
}

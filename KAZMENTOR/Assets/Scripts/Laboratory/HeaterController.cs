using UnityEngine;

public class HeaterController : MonoBehaviour {
    public Animator heaterAnimator;
    public float heatingPower = 5f;
    public bool isHeating = false;
    private MetalProperties currentMetal = null;

    private void OnTriggerEnter2D(Collider2D other) {
        MetalProperties metal = other.GetComponent<MetalProperties>();
        if (metal != null && currentMetal == null) {
            currentMetal = metal;
        }
    }

    private void OnTriggerStay2D(Collider2D other) {
        MetalProperties metal = other.GetComponent<MetalProperties>();
        if (metal != null && metal == currentMetal && isHeating) {
            currentMetal.UpdateTemperature(heatingPower * Time.deltaTime);
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        MetalProperties metal = other.GetComponent<MetalProperties>();
        if (metal != null && metal == currentMetal) {
            currentMetal = null;
        }
    }

    public void StartHeating() {
        isHeating = true;
        heaterAnimator.SetBool("IsHeating", true);
        AudioManager.Instance.PlayButtonSound(); // Воспроизведение звука кнопки
        AudioManager.Instance.PlayHeatingSound();            // Воспроизведение звука нагрева
    }

    public void StopHeating() {
        isHeating = false;
        heaterAnimator.SetBool("IsHeating", false);
        AudioManager.Instance.PlayButtonSound(); // Воспроизведение звука кнопки
        AudioManager.Instance.StopAudioClip(AudioManager.Instance.heating);             // Остановка звука нагрева
    }
}

using UnityEngine;
using TMPro;

public class StopwatchController : MonoBehaviour {
    public TextMeshProUGUI timerText;
    private float elapsedTime = 0f;
    private bool isRunning = false;

    public void StartTimer() {
        isRunning = true;
        AudioManager.Instance.PlayLaboratoryInterfaceSound(); // Воспроизведение звука
    }

    public void StopTimer() {
        isRunning = false;
        AudioManager.Instance.PlayLaboratoryInterfaceSound(); // Воспроизведение звука
    }

    public void ResetTimer() {
        isRunning = false;
        elapsedTime = 0f;
        UpdateTimerDisplay();
        AudioManager.Instance.PlayLaboratoryInterfaceSound(); // Воспроизведение звука
    }

    void Update() {
        if (isRunning) {
            elapsedTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    private void UpdateTimerDisplay() {
        int minutes = Mathf.FloorToInt(elapsedTime / 60F);
        int seconds = Mathf.FloorToInt(elapsedTime % 60F);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 100F) % 100F);
        timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
    }
}

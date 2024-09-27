using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {
    public int sceneNumber;

    public void SwitchScene() {
        AudioManager.Instance.PlayButtonSound();
        if (sceneNumber >= 0 && sceneNumber < SceneManager.sceneCountInBuildSettings) {
            if (sceneNumber == 3) {
                AudioManager.Instance.StopAudioClip(AudioManager.Instance.universeMelody);
                AudioManager.Instance.StopAudioClip(AudioManager.Instance.laboratoryVibe);
            }

            else if (sceneNumber == 6) {
                AudioManager.Instance.StopAudioClip(AudioManager.Instance.outside);
                AudioManager.Instance.StopAudioClip(AudioManager.Instance.dialogue);
            }

            SceneManager.LoadScene(sceneNumber);
        } else {
            Debug.LogError("—цена с указанным номером не существует в Build Settings.");
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour {
    public int sceneNumber;

    public void SwitchScene() {
        if (sceneNumber >= 0 && sceneNumber < SceneManager.sceneCountInBuildSettings) {
            SceneManager.LoadScene(sceneNumber);
        } else {
            Debug.LogError("����� � ��������� ������� �� ���������� � Build Settings.");
        }
    }
}

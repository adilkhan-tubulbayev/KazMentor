using UnityEngine;

public class MainMenu : MonoBehaviour {
    public CameraTransition cameraTransition; // ������ �� ��������� ��� ����������� ������

    public void PlayGame() {
        AudioManager.Instance.PlayButtonSound();
        cameraTransition.StartTransitionWithFade(); // ������ �������� � �����������
    }

    public void QuitGame() {
        AudioManager.Instance.PlayButtonSound();
        Application.Quit();
    }
}

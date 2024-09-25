using UnityEngine;

public class MainMenu : MonoBehaviour {
    public CameraTransition cameraTransition; // Ссылка на компонент для зумирования камеры

    public void PlayGame() {
        AudioManager.Instance.PlayButtonSound();
        cameraTransition.StartTransitionWithFade(); // Запуск анимации с затемнением
    }

    public void QuitGame() {
        AudioManager.Instance.PlayButtonSound();
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuSoundManager : MonoBehaviour
{
    private void Start() {
        AudioManager.Instance.PlayMainMenuSound();
    }
}

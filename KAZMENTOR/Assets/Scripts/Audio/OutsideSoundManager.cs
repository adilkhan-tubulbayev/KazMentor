using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutsideSoundManager : MonoBehaviour
{
    private void Start() {
        AudioManager.Instance.StopAudioClip(AudioManager.Instance.mainMenu);
        AudioManager.Instance.PlayOutsideSound();
    }
}

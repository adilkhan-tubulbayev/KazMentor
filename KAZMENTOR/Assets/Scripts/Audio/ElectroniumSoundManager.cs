using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroniumSoundManager : MonoBehaviour
{
    private void Start() {
        AudioManager.Instance.StopAudioClip(AudioManager.Instance.universeMelody);
        AudioManager.Instance.PlayOutsideSound();
        AudioManager.Instance.PlayElectricFieldOnSound();
    }
}

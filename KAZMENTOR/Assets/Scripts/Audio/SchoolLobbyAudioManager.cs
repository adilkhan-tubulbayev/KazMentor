using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SchoolLobbyAudioManager : MonoBehaviour
{
    private void Start() {
        AudioManager.Instance.StopAudioClip(AudioManager.Instance.outside);
    }
}
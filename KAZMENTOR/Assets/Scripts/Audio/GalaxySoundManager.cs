using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GalaxySoundManager : MonoBehaviour
{
    private void Start() {
        AudioManager.Instance.PlayUniverseMelodySound();
    }
}

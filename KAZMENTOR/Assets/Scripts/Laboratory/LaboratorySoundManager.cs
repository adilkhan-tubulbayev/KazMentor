using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaboratorySoundManager : MonoBehaviour
{
    private void Start() {
        AudioManager.Instance.PlayLaboratoryVibeSound();
    }
}

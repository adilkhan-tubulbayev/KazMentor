using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScale : MonoBehaviour
{

    private Vector3 originalScale;
    [SerializeField] float buttonScale = 1.1f;

    private void Start() {
        originalScale = transform.localScale;
    }

    private void OnMouseEnter() {
        transform.localScale = originalScale * buttonScale;
    }

    private void OnMouseExit() {
        transform.localScale = originalScale;
    }
}

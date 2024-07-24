using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour {
    public Animator barrierAnimator; // ������ �� �������� �������
    public GameObject[] canvases; // ������ ��������, ������� ����� ���������
    public GameObject Player; // ������ �� ������ ������
    private Player playerScript; // ������ �� ������ ������

    private bool isCircuitCorrect = false;
    private bool isTextCorrect = false;

    private void Start() {
        if (Player != null) {
            playerScript = Player.GetComponent<Player>();
        } else {
            Debug.LogError("Player is not assigned in the inspector!");
        }
    }

    public void SetCircuitCorrect(bool value) {
        isCircuitCorrect = value;
        CheckAllConditions();
    }

    public void SetTextCorrect(bool value) {
        isTextCorrect = value;
        CheckAllConditions();
    }

    private void CheckAllConditions() {
        if (isCircuitCorrect && isTextCorrect) {
            if (barrierAnimator != null && barrierAnimator.HasParameter("BarrierOff")) {
                barrierAnimator.SetTrigger("BarrierOff"); // ������ �������� BarrierOff
            } else {
                Debug.LogError("�������� �� �������� ��������� ��� �������� BarrierOff �����������.");
            }

            // ��������, ��� playerScript ���������� ����� �������� � ����
            if (playerScript != null) {
                playerScript.isDialogueActive = false; // ������������ �������� ���������
            }

            // �������� ���� ��������
            StartCoroutine(CloseCanvasesAfterAnimation());
        }
    }

    private IEnumerator CloseCanvasesAfterAnimation() {
        yield return new WaitForSeconds(1); // ���� ���������� ��������, ��������� ������ ����� ��������

        foreach (var canvas in canvases) {
            canvas.SetActive(false); // ��������� ��� �������
        }
    }
}

// ��������������� ����� ��� �������� ���������
public static class AnimatorExtensions {
    public static bool HasParameter(this Animator animator, string paramName) {
        foreach (AnimatorControllerParameter param in animator.parameters) {
            if (param.name == paramName) {
                return true;
            }
        }
        return false;
    }
}

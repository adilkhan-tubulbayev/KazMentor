using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIMenu : MonoBehaviour
{
    public int sceneBuildIndex = 3; // ���������, ��� ������ ���������� �� 3

    public void ExitGame() {
        SceneManager.LoadScene(sceneBuildIndex, LoadSceneMode.Single);
    }

}

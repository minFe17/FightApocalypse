using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MouseOption : MonoBehaviour
{
    // Start is called before the first frame update
    public void MouseOpTion()
    {
        Debug.Log("���콺 �ɼ����� �Ѿ");

        SceneManager.LoadScene("MouseOption");
    }
}

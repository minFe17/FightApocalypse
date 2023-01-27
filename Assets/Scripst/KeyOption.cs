using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyOption : MonoBehaviour
{
    public void KeyOpTion()
    {
        Debug.Log("키옵션으로 넘어감");

        SceneManager.LoadScene("KeyOption");
    }
}

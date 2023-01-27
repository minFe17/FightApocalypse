using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Option : MonoBehaviour
{
    public void OpTion()
    {
        Debug.Log("버튼입력");
        
        SceneManager.LoadScene("Option");
    }
}

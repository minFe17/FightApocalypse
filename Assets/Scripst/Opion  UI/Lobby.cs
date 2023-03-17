using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public void GameStart()
    {
        Debug.Log("버튼입력");
        SceneManager.LoadScene("Main");
        
    }

}

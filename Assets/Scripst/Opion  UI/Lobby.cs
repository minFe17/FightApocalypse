using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public void GameStart()
    {
        Debug.Log("��ư�Է�");
        SceneManager.LoadScene("Main");
        
    }

}

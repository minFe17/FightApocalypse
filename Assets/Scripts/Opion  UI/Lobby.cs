using UnityEngine;
using UnityEngine.SceneManagement;

public class Lobby : MonoBehaviour
{
    public void GameStart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("IngameScene");
    }

    public void ExitGame()
    {
        Application.Quit();
    }

}

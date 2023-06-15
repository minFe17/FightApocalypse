using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;

public class Lobby : MonoBehaviour
{
    public void Start()
    {
        GenericSingleton<SoundManager>.Instance.SoundController.StartBGM();
    }

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

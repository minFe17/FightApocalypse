using UnityEngine;
using UnityEngine.SceneManagement;

public class GameClearUI : MonoBehaviour
{
    public void ToLobbyButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Lobby");
    }

    public void RegameButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("IngameScene");
    }
}

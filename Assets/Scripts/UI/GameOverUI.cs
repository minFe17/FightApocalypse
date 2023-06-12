using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] Text _waveText;

    public void ShowGameOverWave(int wave)
    {
        _waveText.text = $"Wave : {wave}";
    }

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

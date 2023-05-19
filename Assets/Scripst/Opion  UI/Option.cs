using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Option : MonoBehaviour
{
    [SerializeField] GameObject _opton;
    [SerializeField] GameObject _soundoption;
    [SerializeField] GameObject _keyoption;
    [SerializeField] GameObject _mouseoption;
    [SerializeField] GameObject _GmPanel;

    void Update()
    {
        if (_mouseoption.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) _mouseoption.SetActive(false);

        }
        else if (_soundoption.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) _soundoption.SetActive(false);
        }
        else if (_keyoption.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) _keyoption.SetActive(false);
        }
        else if (_opton.activeSelf)// 켜져있으면 true, 꺼져있으면 false
        {

            if (Input.GetKeyDown(KeyCode.Escape)) _opton.SetActive(false);

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                _GmPanel.SetActive(true);
                Time.timeScale = 0f;
            }
        }

    }

    public void OpTion()
    {
        _opton.SetActive(true);
    }

    public void KeyOption()
    {
        _keyoption.SetActive(true);
    }

    public void SoundOption()
    {
        _soundoption.SetActive(true);   
    }
    public void MouseOption() 
    {
        _mouseoption.SetActive(true);
    }

    public void YesButton()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void NoButton()
    {
        _GmPanel.SetActive(false);
        Time.timeScale = 1;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackKey : MonoBehaviour
 
{
    [SerializeField] GameObject _GMpanel;
    [SerializeField] GameObject _Option;
    [SerializeField] GameObject _Sound;
    [SerializeField] GameObject _Mouse;
    [SerializeField] GameObject _Key;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (_Mouse.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) _Mouse.SetActive(false);
        }
        else if (_Sound.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape))_Sound.SetActive(false);
        }
        else if (_Key.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Escape)) _Key.SetActive(false);
        }
        else if (_Option.activeSelf)// 켜져있으면 true, 꺼져있으면 false
        {
            
            if (Input.GetKeyDown(KeyCode.Escape)) _Option.SetActive(false);
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                _GMpanel.SetActive(true);
                Time.timeScale = 0;
            }

        }
              
    }
    public void YesButton()
    {
        SceneManager.LoadScene("Lobby");
    }
    public void NoButton()
    {
        _GMpanel.SetActive(false);
        Time.timeScale = 1;
    }
}

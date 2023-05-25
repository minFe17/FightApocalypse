using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackKey : MonoBehaviour
 
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


}

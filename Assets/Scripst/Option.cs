using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class Option : MonoBehaviour
{
    [SerializeField] GameObject _opton;
    [SerializeField] GameObject _soundoption;
   
   
    public void OpTion()
    {
        Debug.Log("버튼입력");
        
        _opton.SetActive(true);

        

       
        

    }
}

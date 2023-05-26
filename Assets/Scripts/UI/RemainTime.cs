using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RemainTime : MonoBehaviour
{
    Text text;
    static float _rTime = 300f;
    void Start()
    {
        text=GetComponent<Text>();
    }

  
    void Update()
    {
        _rTime -= Time.deltaTime;
        if (_rTime < 0)
            _rTime = 0;
        text.text= "남은 시간 :" + Mathf.Round(_rTime);
    }
}

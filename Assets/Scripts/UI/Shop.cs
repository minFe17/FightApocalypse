using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public RectTransform uiGroup;
    public Animator anim;

    public GameObject[] itemObj;
    public int[] itemPrice;
    public Transform[] itemPos;
    public Text talkText;

    Playerdummy enterPlayer;
    public void Enter(Playerdummy playerdummy)
    {
        enterPlayer = playerdummy;
        uiGroup.anchoredPosition = Vector3.zero;
    }

    
    public void Exit()
    {
        //anim.SetTrigger("doHello");
        uiGroup.anchoredPosition = Vector3.down * 1000;
    }

    public void Buy(int index)
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class dummyShop : MonoBehaviour
{
    public RectTransform uiGroup;
    public Animator anim;

    int player;
    public GameObject[] itemObj;
    public int[] itemPrice;
    public Transform[] itemPos;
    public string[] talkData;
    public Text talkText;

    Playerdummy enterdummyPlayer;
    public void Enter(Playerdummy playerdummy)
    {
        enterdummyPlayer = playerdummy;
        uiGroup.anchoredPosition = Vector3.zero;
    }


    public void Exit()
    {
        //anim.SetTrigger("doHello");
        uiGroup.anchoredPosition = Vector3.down * 1500;
    }

    public void Buy(int index)
    {
        int price = itemPrice[index];
        if (price > enterdummyPlayer._coin)
        {
            StopCoroutine(Talk());
            StartCoroutine(Talk());
            return;
        }

        enterdummyPlayer._coin -= price;
        Vector3 ranVec = Vector3.right * Random.Range(-3, 3) + Vector3.forward * Random.Range(-3, 3);
        Instantiate(itemObj[index], itemPos[index].position + ranVec, itemPos[index].rotation);

    }
    IEnumerator Talk()
    {
        talkText.text = talkData[1];
        yield return new WaitForSeconds(2f);
        talkText.text = talkData[0];
    }
}

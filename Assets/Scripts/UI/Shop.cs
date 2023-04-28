using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class Shop : MonoBehaviour
{
    public RectTransform uiGroup;
    public RectTransform UIGroup {  set {  uiGroup = value; } }
    public Animator anim;

    public GameObject[] itemObj;
    public int[] itemPrice;
    public Transform[] itemPos;
    public string[] talkData;
    public Text talkText;
    public Text TalkText {  set { talkText = value; } }

    Player enterPlayer;

    public void Enter(Player player)
    {
        enterPlayer = player;
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
        if(price > enterPlayer._money)
        {
            StopCoroutine(Talk());
            StartCoroutine(Talk());
            return;
        }

        enterPlayer._money -= price;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowMoney(enterPlayer._money);

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

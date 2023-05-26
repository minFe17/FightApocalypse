using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils;

public class Shop : MonoBehaviour
{
    RectTransform uiGroup;
    public RectTransform UIGroup {  set {  uiGroup = value; } }
    

    [SerializeField] GameObject[] itemObj;
    [SerializeField] int[] itemPrice;
    [SerializeField] Transform[] itemPos;
    [SerializeField] string[] talkData;
    Text talkText;
    public Text TalkText {  set { talkText = value; } }

    Player enterPlayer;

    public void Enter(Player player)
    {
        enterPlayer = player;
        uiGroup.anchoredPosition = Vector3.zero;
    }

    
    public void Exit()
    {     
        uiGroup.anchoredPosition = Vector3.down * 1500;
    }

    public void Buy(int index)
    {
        int price = itemPrice[index];
        if(price > enterPlayer.Money)
        {
            StopCoroutine(Talk());
            StartCoroutine(Talk());
            return;
        }

        enterPlayer.Money -= price;
        GenericSingleton<UIManager>.Instance.IngameUI.ShowMoney(enterPlayer.Money);

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

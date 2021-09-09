
using UnityEngine;
using UnityEngine.EventSystems;

public class UseCardArea : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData) // 카드 사용
    {
        //카드 정보를 받아와서 카드 효과를 대상에게 줘야함
        if(BattleManager.instance.playerAct)
        {
            Card droppedCard = eventData.pointerDrag.GetComponent<Card>();
            if(droppedCard != null)
            {
                BattleManager.instance.usingCard = droppedCard;
                BattleManager.instance.userCharacter = BattleManager.instance.GetCurrentTurnChar().index;
                if(droppedCard.HasCardProperty(CardProperty.ChooseTarget))
                {
                    Debug.Log("User Input Required : Choose Single Target");
                    BattleManager.instance.userInput = true;
                    BattleUIManager.instance.EnableCancelUsingCardBtn();
                    droppedCard.GetComponent<Cardpop>().ExplictlyEndDrag(); // prevent card scale bug when cancel using it
                    droppedCard.gameObject.SetActive(false);
                }
                else
                {
                    BattleManager.instance.CardEffectStep();
                }
            }
        }
    }
}

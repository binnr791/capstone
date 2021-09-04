using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectTargetArea : MonoBehaviour, IDropHandler, IPointerDownHandler
{
    public int charIndex; // 외부에서 초기화하기, 몇 번째 캐릭터를 선택했는지 전달하기 위해 필요한 변수
    public bool isEnemyArea;
    public static int usableCost;

    private void Awake()
    {
    }

    private void Start()
    {
        charIndex = GetComponent<RectTransform>().parent.GetComponent<Character>().index; // 이 라인이 에러나면 부모-자식 구조를 바꿀것.
    }

    public void OnPointerDown(PointerEventData eventData) // 대상 지정
    {
        if(BattleManager.instance.userInput == true)
        {
            Debug.Log("User Input, target Index : " + charIndex.ToString());
            BattleManager.instance.targetCharacter = charIndex;
            BattleManager.instance.userInput = false;
            BattleUIManager.instance.DisableCancelUsingCardBtn();
            BattleManager.instance.CardEffectStep();
        }
    }

    public void OnDrop(PointerEventData eventData) // 카드 사용
    {

     //   if (아군)
     //   {
     //       if(card id =  1,3){
     //           대상 아군에게 카드 효과를 준다;
     //           Destroy(eventData.pointerDrag);
     //       }
     //       else (){
     //           대상에겐 사용 불가능하다는 알림을 주고 카드를 패로 돌려보냄;
     //       }
     //   }
     //   if (적)
     //   {
     //       if(card id = 0,2,4){
     //           대상 적에게 카드 효과를 준다;
     //           Destroy(eventData.pointerDrag);
     //       }
     //       else (){
     //           대상에겐 사용 불가능하다는 알림을 주고 카드를 패로 돌려보냄;
     //       }
     //   }
        //카드 정보를 받아와서 카드 효과를 대상에게 줘야함
        if(!isEnemyArea)
        {
            if(BattleManager.instance.playerAct)
            {
                Card droppedCard = eventData.pointerDrag.GetComponent<Card>();
                if(GameManager.instance.checkCost && droppedCard.cost > usableCost) // 코스트가 부족할때 카드사용을 못하게함.
                {
                    Debug.Log("Not Enough Cost!");
                }
                else if(droppedCard != null)
                {
                    BattleManager.instance.usingCard = droppedCard;
                    BattleManager.instance.userCharacter = charIndex;
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
        else
        {
            Debug.Log("You can't use card to enemy!");
        }
    }
}

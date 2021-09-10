using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectTargetArea : MonoBehaviour, IPointerDownHandler
{
    public int charIndex; // 외부에서 초기화하기, 몇 번째 캐릭터를 선택했는지 전달하기 위해 필요한 변수
    public bool isEnemyArea;
    public static int usableCost;

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
            BattleUIManager.instance.DisableInstruction();
            BattleUIManager.instance.DisableCancelUsingCardBtn();
            BattleManager.instance.CardEffectStep();
        }
    }
}

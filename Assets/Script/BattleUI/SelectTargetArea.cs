using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectTargetArea : MonoBehaviour, IDropHandler, IPointerUpHandler
{
    public int charIndex; // 외부에서 초기화하기, 몇 번째 캐릭터를 선택했는지 전달하기 위해 필요한 변수

    private void Awake()
    {
        
    }

    public void OnPointerUp(PointerEventData eventData) // 대상 지정
    {
        
    }

    public void OnDrop(PointerEventData eventData) // 카드 사용
    {
        
    }
}

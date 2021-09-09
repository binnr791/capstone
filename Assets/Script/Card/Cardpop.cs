using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cardpop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    // 마우스가 카드 위에 올라가 있거나, 드래그중일때 카드가 2배 커짐 / 카드위치 바꾸는건 아직 X 

    [SerializeField] bool IsCardpointer;
    [SerializeField] bool IsDragging;

    public int index;
    public bool isEnoughCost;

    RectTransform rTransform;
    CanvasGroup canvasGroup;

    // --카드 생성시 할당--
    public RectTransform hand;
    public RectTransform grabbingCard;

    private void Awake()
    {
        rTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(BattleManager.instance.playerAct && isEnoughCost)
        {
            IsDragging = true;
            rTransform.SetParent(grabbingCard);
            rTransform.anchorMin = new Vector2(0.5f, 0.5f); // 앵커 preset 변경
            rTransform.anchorMax = new Vector2(0.5f, 0.5f);
            canvasGroup.blocksRaycasts = false; // 상호작용 off
        }
        else if(BattleManager.instance.playerAct == true && !isEnoughCost)
        {
            Debug.Log("Not Enough Cost To Use The Card");
        }
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        if(BattleManager.instance.playerAct && isEnoughCost)
        {
            ExplictlyEndDrag();
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(BattleManager.instance.playerAct && isEnoughCost)
        {
            // 핸드 트랜스폼(레이아웃)에서 분리
            rTransform.anchoredPosition = (Input.mousePosition); // 위치 갱신
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        IsCardpointer = true;
    }
    public void OnPointerExit(PointerEventData data)
    {
        IsCardpointer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsDragging | IsCardpointer)
        {
            transform.localScale = Vector3.one * (2);

        }
        else
        {
            transform.localScale = Vector3.one;
        }
    }

    public void ExplictlyEndDrag()
    {
        IsDragging = false;
        this.rTransform.SetParent(hand); // 핸드 트랜스폼과 결합
        this.rTransform.SetSiblingIndex(index); // 드래그 떼면 원래 위치로 되돌리기
        canvasGroup.blocksRaycasts = true; // 상호작용 on
    }
}

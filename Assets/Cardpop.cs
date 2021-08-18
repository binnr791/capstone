using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cardpop : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    // 마우스가 카드 위에 올라가 있거나, 드래그중일때 카드가 2배 커짐 / 카드위치 바꾸는건 아직 X 

    bool IsCardpointer;
    bool IsDragging;

    public void OnBeginDrag(PointerEventData eventData)
    {
        IsDragging = true;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        IsDragging = false;
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        IsCardpointer = true;
    }
    public void OnPointerExit(PointerEventData data)
    {
        IsCardpointer = false;
    }
    public void OnDrag(PointerEventData eventData)
    {

    }
        // Start is called before the first frame update
    void Start()
    {
        
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class ChooseCharButton : MonoBehaviour, IPointerEnterHandler
{
    public int index;

    public void OnPointerEnter(PointerEventData eventData)
    {
        ChooseCharacterManager.instance.UpdateCharInfo(index);
    }
}

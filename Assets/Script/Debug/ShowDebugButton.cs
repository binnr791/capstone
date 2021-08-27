using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ShowDebugButton : MonoBehaviour
{
    public bool childEnabled;
    RectTransform rectTransform;

    Button button;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        button = GetComponent<Button>();
        childEnabled = !childEnabled;
        EnableChild(); // 모든 자식 오브젝트를 child enabled 한 후 시작

        button.onClick.AddListener(() => EnableChild());
    }

    private void EnableChild()
    {
        childEnabled = !childEnabled;
        for(int i = 0; i < rectTransform.childCount; i++)
        {
            if(i != 0)
            {
                rectTransform.GetChild(i).gameObject.SetActive(childEnabled);
            }
        }

        if(childEnabled)
        {
            rectTransform.GetChild(0).GetComponent<Text>().text = "HIDE";
        }
        else
        {
            rectTransform.GetChild(0).GetComponent<Text>().text = "SHOW";
        }
    }
}

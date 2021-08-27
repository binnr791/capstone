using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    public static BattleUIManager instance;

    [SerializeField] Text UICycle; // 인스펙터 활용

    [SerializeField] GameObject cancelUsingCardBtn; // button = btn

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void UpdateCycle(int cycleNum)
    {
        UICycle.text = "Cycle : " + cycleNum.ToString();
    }

    public void EnableCancelUsingCardBtn()
    {
        cancelUsingCardBtn.SetActive(true);
    }
    public void DisableCancelUsingCardBtn()
    {
        cancelUsingCardBtn.SetActive(false);
    }



    public void EnableChooseResource()
    {

    }

    public void DisableChooseResource()
    {
        
    }
}

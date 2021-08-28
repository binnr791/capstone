using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    public static BattleUIManager instance;
    public GameObject deckbutton;
    public GameObject d_backbutton;
    [Header("Notice")]
    public GameObject subbox;
    public Text subintext;


    private WaitForSeconds _UIDelay = new WaitForSeconds(2.3f);


    [SerializeField] Text UICycle; // 인스펙터 활용

    [SerializeField] GameObject cancelUsingCardBtn; // button = btn

    private void Awake()
    {
        d_backbutton.SetActive(false);
        subbox.SetActive(false);
        if (instance == null)
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

    //public void notice

    public void showdeck()
    {
        deckbutton.SetActive(false);
        d_backbutton.SetActive(true);
    }
    public void backtofield()
    {
        deckbutton.SetActive(true);
        d_backbutton.SetActive(false);
    }

    public void EnableChooseResource()
    {

    }

    public void DisableChooseResource()
    {
        
    }
}

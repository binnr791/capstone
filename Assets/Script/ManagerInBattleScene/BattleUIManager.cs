using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    public static BattleUIManager instance;
    public GameObject deckbutton;
    public GameObject d_backbutton;
    [SerializeField] Text UICycle; // 인스펙터 활용

    [SerializeField] GameObject cancelUsingCardBtn; // button = btn
    [Header("Notice")]
    public GameObject noticebox;
    public Text noticetext;


    private WaitForSeconds _UIDelay = new WaitForSeconds(2.3f);


    

    private void Awake()
    {
        d_backbutton.SetActive(false);
        noticebox.SetActive(false);
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

    public void notice(string message)
    {
        noticetext.text = message;
        noticebox.SetActive(false);
        StopAllCoroutines();
        StartCoroutine(noticeDelay());
    }

    IEnumerator noticeDelay()
    {
        noticebox.SetActive(true);
        yield return _UIDelay;
        noticebox.SetActive(false);
    }

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

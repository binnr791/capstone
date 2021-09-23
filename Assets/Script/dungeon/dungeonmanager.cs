using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class dungeonmanager : MonoBehaviour
{

    public static dungeonmanager instance;

    public GameObject deckbutton;
    public GameObject showDeckPanel;
    public GameObject charbutton;

    [Header("Instruction")] // 알림창
    public GameObject instructionBox;
    public Text instructionText;

    [Header("Notice")] // 알림창
    public GameObject noticebox;
    public Text noticetext;

    private WaitForSeconds _UIDelay = new WaitForSeconds(2.3f);
    // Start is called before the first frame update

    private void Awake()
    {
        showDeckPanel.SetActive(false);
        instructionBox.SetActive(false);
        noticebox.SetActive(false);
        if (instance == null)
        {
            instance = this;
        }
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
        showDeckPanel.SetActive(true);
        //카드 목록 가져오기
    }

    public void showchar()
    {
        charbutton.SetActive(false);
        showDeckPanel.SetActive(true);
        //캐릭터 목록 가져오기
    }
    public void backtofield()
    {
        deckbutton.SetActive(true);
        showDeckPanel.SetActive(false);
        //카드&캐릭터 정보 끄기
    }
}

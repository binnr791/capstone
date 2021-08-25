using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class healthpoint : MonoBehaviour
{
    [SerializeField] private Status status; // 캐릭터 스테이터스에서 레퍼런스 가져오기
    [SerializeField] private Slider HealthBar;

    public Text hppoint;
    public Text maxpoint;
    public Text stamina;
    public Text block;

    void Start()
    {

    }


    void Update()
    {
        
        HealthBar.value = ((float) status.hp / (float) status.maxhp) * 100 ; 


        maxpoint.text = status.maxhp.ToString();
        hppoint.text = status.hp.ToString();
        stamina.text = status.stamina.ToString();
        block.text = status.block.ToString();
    }
}

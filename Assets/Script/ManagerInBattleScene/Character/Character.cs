using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character : MonoBehaviour
{
    public int maxhp;
    public int hp;
    public int block; // 방어도
    public int stamina;
    public int baseSpeed;
    public int nowSpeed;
    public enum Faction { Player, Enemy };
    public Faction faction;
    public int index;  // 여기까지 Status스크립트에 있던 변수

    public Slider HealthBar; //여기부터 health point 스크립트의 내용

    public Text hppoint;
    public Text maxpoint;
    public Text staminapoint;
    public Text blockpoint;


    void Update()
    {

        HealthBar.value = ((float) hp / (float) maxhp) * 100;

        maxpoint.text = maxhp.ToString();
        hppoint.text = hp.ToString();
        staminapoint.text = stamina.ToString();
        blockpoint.text = block.ToString();
    }
}
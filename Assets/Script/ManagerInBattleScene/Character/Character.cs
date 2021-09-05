using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Character : MonoBehaviour
{
    public int index;  // 여기까지 Status스크립트에 있던 변수

    [Header("UI")]
    public Slider HealthBar; //여기부터 health point 스크립트의 내용

    public Text hppoint;
    public Text maxpoint;
    public Text staminapoint;
    public Text blockpoint;

    [Header("Debug")]
    public Text charNameText;

    public Status stat; // 직렬화를 하려고 stat 클래스를 만듦.
    public Faction faction;

    [HideInInspector] public string charName;

    private void Start()
    {
        charNameText.text = charName;
    }

    void Update()
    {

        HealthBar.value = ((float) stat.hp / (float) stat.maxhp) * 100;

        maxpoint.text = stat.maxhp.ToString();
        hppoint.text = stat.hp.ToString();
        staminapoint.text = stat.stamina.ToString();
        blockpoint.text = stat.block.ToString();
    }
}

public enum Faction
{
    Player, Enemy
};
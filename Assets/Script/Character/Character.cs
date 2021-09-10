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
    public Image nowturn;

    [Header("Debug")]
    public Text charNameText;

    public List<StatusEffect> statusEffects;
    public Stat stat; // 직렬화를 하려고 stat 클래스를 만듦.
    public Faction faction;

    protected delegate void turnEndFunc();
    protected turnEndFunc TurnEndFunc;

    [HideInInspector] public string charName;

    private void Awake()
    {
        statusEffects = new List<StatusEffect>();
        TurnEndFunc += UpdateStatusEffect;
    }

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

    public void battleStart()
    {
        stat.stamina = 4;
    }

    public void TurnEndEvent()
    {
        TurnEndFunc();
    }

    public void UpdateStatusEffect()
    {
        for(int i = 0; i < statusEffects.Count; i++)
        {
            statusEffects[i].remainTurn -= 1;
            if(statusEffects[i].remainTurn <= 0)
            {
                statusEffects.RemoveAt(i);
            }
            i -= 1;
        }
    }
    
}

public enum Faction
{
    Player, Enemy
};
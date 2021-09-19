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

    public GameObject statusEffectBar;

    [Header("Debug")]
    public Text charNameText;

    public int maxStatusEffectCount = 5;
    public List<StatusEffect> statusEffects;
    public Stat stat; // 직렬화를 하려고 stat 클래스를 만듦.
    public Faction faction;

    protected delegate void turnEndFunc();
    protected turnEndFunc TurnEndFunc;

    [HideInInspector] public string charName;

    private void Awake()
    {
        statusEffects = new List<StatusEffect>();
        TurnEndFunc += UpdateStatusEffectLogic;
    }

    private void Start()
    {
        charNameText.text = charName;
        UpdateStatusEffectUI();
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

    public bool HasStatusEffect(StatusEffectID statusID)
    {
        for(int i = 0; i < statusEffects.Count; i++)
        {
            if(statusEffects[i] != null && statusEffects[i].id == statusID)
            {
                return true;
            }
        }
        return false;
    }

    public void AddStatusEffect(StatusEffectID statusID, int _remainTurn)
    {
        for(int i = 0; i < statusEffects.Count; i++)
        {
            if(statusEffects[i] != null && statusEffects[i].id == statusID)
            {
                statusEffects[i].remainTurn += _remainTurn;
                UpdateStatusEffectUI();
                return;
            }
        }
        if(statusEffects.Count >= maxStatusEffectCount) // maximum status effect count is 5
        {
            return;
        }
        statusEffects.Add(StatusEffectManager.instance.createStatusEffect(statusID, _remainTurn, this));
        UpdateStatusEffectUI();
    }

    public void UpdateStatusEffectLogic()
    {
        // update logic
        for(int i = 0; i < statusEffects.Count; i++)
        {
            statusEffects[i].OnTurnEnd?.Invoke(this); // ? is null checking
            statusEffects[i].remainTurn -= 1;
            if(statusEffects[i].remainTurn <= 0)
            {
                statusEffects.RemoveAt(i);
            }
            i -= 1;
        }
        UpdateStatusEffectUI();
    }

    public void UpdateStatusEffectUI()
    {
        // update ui
        for(int i = 0; i < statusEffects.Count; i++)
        {
            statusEffectBar.transform.GetChild(i).gameObject.SetActive(true);
            statusEffectBar.transform.GetChild(i).GetComponent<Image>().sprite = ResourceManager.instance.GetStatusEffectSprite(statusEffects[i].id);
            statusEffectBar.transform.GetChild(i).GetChild(0).GetComponent<Text>().text = statusEffects[i].remainTurn.ToString();
        }

        for(int i = statusEffects.Count; i < maxStatusEffectCount; i++)
        {
            statusEffectBar.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
    
}

public enum Faction
{
    Player, Enemy
};
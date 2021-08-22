using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EffectLibrary))]

public class CardEffect : MonoBehaviour
{
    public Dictionary<int, CardEffectInfo> idToEffect;
    public delegate void effectFunc();

    EffectLibrary effectLibrary;

    
    private void Awake()
    {
        effectLibrary = GetComponent<EffectLibrary>();

        idToEffect = new Dictionary<int, CardEffectInfo>();

        List<CardProperty> chooseTargetProperty = new List<CardProperty>();
        chooseTargetProperty.Add(CardProperty.ChooseTarget);

        List<CardProperty> recycleProperties = new List<CardProperty>();
        recycleProperties.Add(CardProperty.ChooseTarget);
        recycleProperties.Add(CardProperty.Recycle);


        idToEffect[0] = new CardEffectInfo(BloodSucking, chooseTargetProperty);
        idToEffect[1] = new CardEffectInfo(Defense, null);
        idToEffect[2] = new CardEffectInfo(SingleTargetAttack, chooseTargetProperty);
        idToEffect[3] = new CardEffectInfo(SingleTargetHeal, chooseTargetProperty);
        idToEffect[4] = new CardEffectInfo(AttackAllEnemy, null);
        idToEffect[5] = new CardEffectInfo(Dedicatation, chooseTargetProperty);
        idToEffect[6] = new CardEffectInfo(DedicateAndHeal, chooseTargetProperty);

        idToEffect[7] = new CardEffectInfo(RecylceAttack, recycleProperties);

        idToEffect[8] = new CardEffectInfo(HealAllAlly, null);
    }

    public CardEffectInfo GetEffectInfo(int id)
    {
        return idToEffect[id];
    }

    public void BloodSucking()
    {

    }

    public void Defense()
    {

    }

    public void SingleTargetAttack()
    {

    }

    public void SingleTargetHeal()
    {

    }

    public void AttackAllEnemy()
    {
        // int amount = 6;
        // Character user = BattleManager.instance.GetUserCharacter();
        // List<Character> targets = BattleManager.instance.GetAllEnemy();
        // effectLibrary.Attack(user, targets, amount);
    }

    public void Dedicatation()
    {

    }

    public void DedicateAndHeal()
    {

    }

    public void RecylceAttack()
    {

    }

    public void HealAllAlly()
    {

    }
}

public enum CardProperty // 외부에서 흐름 제이에 필요한 카드 property를 체크하는 데 사용
{
    ChooseTarget, Recycle
}
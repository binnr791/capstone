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
        Debug.Log("Use Card : Blood Sucking");
        Character targetChar = BattleManager.instance.GetTargetCharacter();
        Character userChar = BattleManager.instance.GetUserCharacter();

        Character attackTarget = targetChar;
        Character healTarget = userChar;

        effectLibrary.Attack(userChar, attackTarget, 3);
        effectLibrary.Heal(userChar, healTarget, 3);
    }

    public void Defense()
    {
        Debug.Log("Use Card : Defense");
        Character userChar = BattleManager.instance.GetUserCharacter();
        Character target = userChar;

        effectLibrary.Armor(userChar, target, 6);
    }

    public void SingleTargetAttack()
    {
        Debug.Log("Use Card : SingleTargetAttack");
        Character user = BattleManager.instance.GetUserCharcter();// 사용하는 캐릭
        Character targetChar = BattleManager.instance.GetTargetCharacter(); //대상지정 캐릭

        Character attackTarget = targetChar;

        effectLibrary.Attack(user, attackTarget, 6);

    }

    public void SingleTargetHeal()
    {
        Debug.Log("Use Card : SingleTargetHeal");
        Character userChar = BattleManager.instance.GetUserCharcter();//사용하는 캐릭
        Character target = BattleManager.instance.GetTargetCharacter();//대상지정 캐릭

        Character healTarget = userChar;

        effectLibrary.Heal(userChar, healTarget, 6);

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

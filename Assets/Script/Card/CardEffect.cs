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

        idToEffect[10] = new CardEffectInfo(Poison, chooseTargetProperty);
        idToEffect[11] = new CardEffectInfo(Stun, chooseTargetProperty);
        idToEffect[12] = new CardEffectInfo(Weakening, chooseTargetProperty);
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

        effectLibrary.Attack(userChar, attackTarget, 4);
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
        Character user = BattleManager.instance.GetUserCharacter();
        Character targetChar = BattleManager.instance.GetTargetCharacter();


        effectLibrary.Attack(user, targetChar, 6);
    }

    public void SingleTargetHeal()
    {
        Debug.Log("Use Card : SingleTargetHeal");
        Character user = BattleManager.instance.GetUserCharacter();
        Character targetChar = BattleManager.instance.GetTargetCharacter();


        effectLibrary.Heal(user, targetChar, 4);

    }

    public void AttackAllEnemy()
    {
        int amount = 8;
        Character user = BattleManager.instance.GetUserCharacter();
        List<Character> targets = effectLibrary.GetAllEnemies();
        for(int i = 0; i < targets.Count; i++)
        {
            effectLibrary.Attack(user, targets[i], amount);
        }
    }

    public void Dedicatation()
    {

    }

    public void DedicateAndHeal()
    {

    }

    public void RecylceAttack()
    {
        Character user = BattleManager.instance.GetUserCharacter();
        Character targetChar = BattleManager.instance.GetTargetCharacter();

        effectLibrary.Attack(user, targetChar, 7);
    }

    public void HealAllAlly()
    {
        int amount = 3;
        Character user = BattleManager.instance.GetUserCharacter();
        List<Character> targets = effectLibrary.GetAllPlayers();
        for(int i = 0; i < targets.Count; i++)
        {
            effectLibrary.Heal(user, targets[i], amount);
        }
    }

    public void Poison()
    {
        int amount = 3;
        Character target = BattleManager.instance.GetTargetCharacter();
        effectLibrary.AddStatusEffect(target, StatusEffectID.poison, amount);
    }

    public void Stun()
    {
        int amount = 1;
        Character target = BattleManager.instance.GetTargetCharacter();
        effectLibrary.AddStatusEffect(target, StatusEffectID.stun, amount);
    }

    public void Weakening()
    {
        int amount = 2;
        Character target = BattleManager.instance.GetTargetCharacter();
        effectLibrary.AddStatusEffect(target, StatusEffectID.weakening, amount);
    }
}

public enum CardProperty // 외부에서 흐름 제이에 필요한 카드 property를 체크하는 데 사용
{
    ChooseTarget, Recycle
}
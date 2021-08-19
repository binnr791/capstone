using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EffectLibrary))]

public class CardEffect : MonoBehaviour
{
    public Dictionary<int, effectFunc> idToEffect;
    public delegate void effectFunc();

    EffectLibrary effectLibrary;

    
    private void Awake()
    {
        effectLibrary = GetComponent<EffectLibrary>();

        idToEffect = new Dictionary<int, effectFunc>();

        idToEffect[0] = BloodSucking;
        idToEffect[1] = Defense;
        idToEffect[2] = SingleTargetAttack;
        idToEffect[3] = SingleTargetHeal;
        idToEffect[4] = AttackAllEnemy;

    }

    public effectFunc GetEffect(int id)
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
}

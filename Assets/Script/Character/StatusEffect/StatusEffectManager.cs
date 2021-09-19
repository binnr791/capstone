using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StatusEffectManager : MonoBehaviour
{
    public static StatusEffectManager instance;
    Dictionary<StatusEffectID, StatusEffectFunc> statusEffects;
    
    public void PoisonEffect(Character target)
    {
        target.stat.hp -= (int)(target.stat.maxhp * 0.04);
        BattleManager.instance.CheckGameState();
    }

    public void BleedingEffect(Character target)
    {
        target.stat.hp -= 2;
        BattleManager.instance.CheckGameState();
    }

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        StatusEffectFunc poison = new StatusEffectFunc();
        poison.OnTurnEnd = PoisonEffect;

        StatusEffectFunc bleeding = new StatusEffectFunc();
        bleeding.OnTurnEnd = BleedingEffect;

        statusEffects = 
        new Dictionary<StatusEffectID, StatusEffectFunc>()
        {
            {
                StatusEffectID.poison, poison
            },
            {
                StatusEffectID.bleeding, bleeding
            },
            {
                StatusEffectID.stun, null
            },
            {
                StatusEffectID.weakening, null
            },
            {
                StatusEffectID.randomDebuff, null
            },
        };
    }

    public StatusEffect createStatusEffect(StatusEffectID id, int remainTurn, Character target)
    {
        StatusEffect se = new StatusEffect(id, remainTurn, target);
        if(statusEffects[id] != null)
        {
            se.OnTurnEnd = statusEffects[id].OnTurnEnd;
        }

        if(id == StatusEffectID.randomDebuff)
        {
            System.Random random = new System.Random();
            int randomNum = random.Next(0, 2);

            StatusEffect se2;

            switch(randomNum)
            {
                case 0:
                    se2 = new StatusEffect(StatusEffectID.poison, remainTurn, target);
                    break;
                case 1:
                    se2 = new StatusEffect(StatusEffectID.stun, remainTurn, target);
                    break;
                case 2:
                    se2 = new StatusEffect(StatusEffectID.weakening, remainTurn, target);
                    break;
                default:
                    Debug.LogError("Random Status Effect Error");
                    return null;
            }
        }
        return se;
    }
    
    public class StatusEffectFunc
    {
        public Action<Character> OnTurnEnd;
        public Action<Character> OnTurnStart;

        public StatusEffectFunc() // setting up default value
        {
            OnTurnEnd = null;
            OnTurnStart = null; // not use
        }
    }
}

public enum StatusEffectID
{
    none, poison, bleeding, stun, weakening, randomDebuff
}
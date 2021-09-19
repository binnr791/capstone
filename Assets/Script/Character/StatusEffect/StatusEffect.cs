using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class StatusEffect
{
    public string effectName;
    public StatusEffectID id;
    public int remainTurn;
    public Character character;

    public Action<Character> OnTurnEnd;

    public StatusEffect(StatusEffectID _id, int _remainTurn, Character _character)
    {
        id = _id;
        remainTurn = _remainTurn;
        character  = _character;
    }

}

using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class StatusEffect
{
    public string effectName;
    public int id;
    public int remainTurn;
    
    public StatusEffect(int _remainTurn)
    {
        remainTurn = _remainTurn;
    }
}
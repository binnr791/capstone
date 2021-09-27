using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Runtime.CompilerServices;

public class CharacterPerkManager
{
    // callermembername get name of caller method
    public delegate void perkFunction(ref int amount, [CallerMemberName] string caller = "");
    private Dictionary<PerkID, perkFunction> _perkDict;
    public Dictionary<PerkID, perkFunction> perkDict { get => _perkDict; private set => _perkDict = value; }

    public CharacterPerkManager()
    {
        _perkDict = new Dictionary<PerkID, perkFunction>();
        _perkDict[PerkID.increaseHeal] = increaseHeal;
    }

    public void increaseHeal(ref int amount, [CallerMemberName] string caller = "")
    {
        // StackTrace stackTrace = new StackTrace(); 
        // if(stackTrace.GetFrame(1).GetMethod() == )
        if(caller == "Heal")
        {
            amount += 2;
        }
    }
}

public enum PerkID
{
    none, increaseHeal
}

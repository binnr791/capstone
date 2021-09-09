using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public struct Status
{
    public int maxhp;
    public int hp;
    public int block; // 방어도
    public int _stamina;
    public int stamina
    {
        get {return _stamina;}
        set 
        {
            _stamina = value;
            CardManager.instance.UpdateAvailableHand();
        }
    }
    public int baseSpeed;
    public int nowSpeed;
}

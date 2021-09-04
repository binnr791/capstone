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
            if(true) // 이부분 어떻게 해야할지 모르겠음
            {
                SelectTargetArea.usableCost = _stamina;
                Debug.Log("Useable Cost is" +_stamina);
                Debug.Log(BattleManager.instance.turnList[BattleManager.instance.turnNum].GetComponent<Character>().index);
            }
        }
    }
    public int baseSpeed;
    public int nowSpeed;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTurn : MonoBehaviour
{
    // Start is called before the first frame update
    public void TurnEnd()
    {
        if(BattleManager.instance.playerAct)
        {
            StartCoroutine(BattleManager.instance.EndTurn());
        }
        else
        {
            Debug.Log("Not Player Turn");
        }
    }
}

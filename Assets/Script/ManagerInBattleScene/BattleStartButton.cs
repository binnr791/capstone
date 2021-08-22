using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleStartButton : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartBattle()
    {
        BattleManager.instance.BattleStart();
        //Instantiate(Resources.Load("Prefab/BattleManager"), new Vector3(0,0,0),Quaternion.identity);
        Active(false);
    }
    public void Active(bool isActive)
    {
        gameObject.SetActive(isActive);
    }
}

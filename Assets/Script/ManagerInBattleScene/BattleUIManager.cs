using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    public static BattleUIManager instance;

    [SerializeField] Text UICycle; // 인스펙터 활용

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    public void UpdateCycle(int cycleNum)
    {
        UICycle.text = "Cycle : " + cycleNum.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawCard : MonoBehaviour
{
    public void DrawButton()
    {
        StartCoroutine(BattleManager.instance.Draw());
    }
}

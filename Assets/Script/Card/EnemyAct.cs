using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이 클래스는 적의 행동을 정의하는 클래스입니다.
/// CardEffect와 동일하게, EffectLibrary를 사용하여 구현합니다.
/// </summary>

public class EnemyAct : MonoBehaviour
{
    public Dictionary<int, actFunc> idToAct;
    public delegate void actFunc();

    EffectLibrary effectLibrary;

    private void Awake()
    {
        effectLibrary = GetComponent<EffectLibrary>();

        idToAct = new Dictionary<int, actFunc>();

        idToAct[0] = EAttack;
    }

    public void EAttack()
    {
        Character targetChar = BattleManager.instance.GetTargetCharacter();
        Character userChar = BattleManager.instance.GetUserCharacter();
        effectLibrary.Attack(userChar, targetChar, 3);
    }
}

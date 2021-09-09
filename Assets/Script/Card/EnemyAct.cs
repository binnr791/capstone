using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이 클래스는 적의 행동을 정의하는 클래스입니다.
/// CardEffect와 동일하게, EffectLibrary를 사용하여 구현합니다.
/// </summary>

public class EnemyAct : MonoBehaviour
{
    private static EnemyAct instance;
    public List<Character> charactersInfo;
    public Dictionary<int, actFunc> idToAct;
    public delegate void actFunc();
    public List <Character> EnemyTarget;

    EffectLibrary effectLibrary;

    private void Awake()
    {
        effectLibrary = GetComponent<EffectLibrary>();

        idToAct = new Dictionary<int, actFunc>();

        idToAct[0] = EAttack;
        idToAct[1] = EHeal;
        idToAct[2] = EBlock;

    }

    private Character Identification()
    {
        
        for(int n = 0 ; ; n++)
        {
            if(BattleManager.instance.charactersInfo[n].faction == Faction.Player)
            {
                EnemyTarget.Add (BattleManager.instance.charactersInfo[n]);
            }

        }
        //return charactersInfo[EnemyTarget];
    }

    

    public void EAttack()
    {
        Character targetChar = EnemyAct.instance.Identification();
        Character userChar = BattleManager.instance.GetUserCharacter();
        effectLibrary.Attack(userChar, targetChar, 3);
    }

    public void EHeal()
    {
        Character targetChar = BattleManager.instance.GetTargetCharacter();
        Character userChar = BattleManager.instance.GetUserCharacter();
        effectLibrary.Heal(userChar, targetChar, 3);

    }

    public void EBlock()
    {
        Character targetChar = BattleManager.instance.GetTargetCharacter();
        Character userChar = BattleManager.instance.GetUserCharacter();
        effectLibrary.Armor(userChar, targetChar, 3);

    }

    // 질문사항

    // 적이 행동할 때 같은 팀 공격하지 않게 하기
    // 어디서 호출해야할지를 모르겠다
    // 타겟 지정을 방법
    // private 메소드
    // 적군 아군 for문
    // 타겟만 신경쓰기
    // charactersInfo 이용하기
}

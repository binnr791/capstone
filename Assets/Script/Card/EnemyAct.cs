using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 이 클래스는 적의 행동을 정의하는 클래스입니다.
/// CardEffect와 동일하게, EffectLibrary를 사용하여 구현합니다.
/// </summary>

public class EnemyAct : MonoBehaviour
{
    //private static EnemyAct instance;
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
        Character targetChar = GetRandomPlayer();
        Character userChar = BattleManager.instance.GetUserCharacter();
        effectLibrary.Attack(userChar, targetChar, 3);
        LogEnemyAct("Enemy Attack", userChar, targetChar);
    }

    public void EHeal()
    {
        Character targetChar = GetRandomEnemy();
        Character userChar = BattleManager.instance.GetUserCharacter();
        effectLibrary.Heal(userChar, targetChar, 3);
        LogEnemyAct("Enemy Heal", userChar, targetChar);
    }

    public void EBlock()
    {
        Character targetChar = GetRandomEnemy();
        Character userChar = BattleManager.instance.GetUserCharacter();
        effectLibrary.Armor(userChar, targetChar, 3);
        LogEnemyAct("Enemy Block", userChar, targetChar);
    }

    public actFunc GetRandomEnemyAction()
    {
        return idToAct[Random.Range(0, idToAct.Count)];
    }

    public Character GetRandomPlayer() // enemy 입장에서의 enemy
    {
        List<Character> players = new List<Character>();

        List<Character> characters = BattleManager.instance.charactersInfo;
        for(int n = 0 ; n < characters.Count; n++)
        {
            if(BattleManager.instance.charactersInfo[n].faction == Faction.Player)
            {
                players.Add (BattleManager.instance.charactersInfo[n]);
            }
        }
        return players[Random.Range(0, players.Count)];
    }

    public Character GetRandomEnemy() // enemy 입장에서의 ally
    {
        List<Character> enemies = new List<Character>();

        List<Character> characters = BattleManager.instance.charactersInfo;
        for(int n = 0 ; n < characters.Count; n++)
        {
            if(BattleManager.instance.charactersInfo[n].faction == Faction.Enemy)
            {
                enemies.Add (BattleManager.instance.charactersInfo[n]);
            }
        }
        return enemies[Random.Range(0, enemies.Count)];
    }

    public void LogEnemyAct(string action, Character userChar, Character targetChar)
    {
        Debug.Log(action + ", User : " + userChar.charName.ToString() + ", Target : " + targetChar.charName.ToString());
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

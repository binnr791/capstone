using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLibrary : MonoBehaviour
{
    // 메소드 이름 수정시 주의 : CharacterPerkManager도 수정해야 함
    CharacterPerkManager perkManager;

    private void Awake()
    {
        perkManager = new CharacterPerkManager();    
    }

    public void Attack(Character user, Character target, int amount) // user는 사용 캐릭터이고, target은 대상 캐릭터다.
    {
        if(perkManager.perkDict[user.perk] != null) // 코드 중복 제거한다고 합치지 말 것
        {
            perkManager.perkDict[user.perk](ref amount);
        }
        else
        {
            Debug.Log("User : " + user + "'s perk is not initialized.");
        }
        
        if(user.HasStatusEffect(StatusEffectID.weakening))
        {
            amount = (int)(amount * 0.75f);
        }
        if(target.stat.block > 0)
        {
            if(target.stat.block >= amount)
            {
                target.stat.block -= amount;
            }
            else // 방어도 초과 피해량 계산
            {
                int overDmg = Mathf.Abs(target.stat.block - amount);
                target.stat.block = 0;
                target.stat.hp -= overDmg;
            }
        }
        else
        {
            target.stat.hp -= amount;
        }
    }

    //캐릭터 힐
    public void Heal(Character user, Character target, int amount)
    {
        if(perkManager.perkDict[user.perk] != null)
        {
            perkManager.perkDict[user.perk](ref amount);
        }
        else
        {
            Debug.Log("User : " + user + "'s perk is not initialized.");
        }

        if (target.stat.hp <= 0) // 대상이 체력이 0이면 체력 회복 불가
        {
            target.stat.hp = 0;
        }

        target.stat.hp += amount;

        if (target.stat.hp > target.stat.maxhp) //최대 hp넘게 힐 불가
        {
            target.stat.hp = target.stat.maxhp;
        }

    }

    public void DrawACard()  //카드 드로우
    {
        //덱(리스트)은 오름차순 정렬이 되어있으므로, 랜덤 함수를 호출한다
        //뽑은 카드는 덱에서 제거한다.
        CardManager.instance.DrawCard();
    }

    public void Armor(Character user, Character target, int amount) //방어도 증가
    {
        if(perkManager.perkDict[user.perk] != null) // 코드 중복 제거한다고 합치지 말 것
        {
            perkManager.perkDict[user.perk](ref amount);
        }
        else
        {
            Debug.Log("User : " + user + "'s perk is not initialized.");
        }
        //파라미터로 들어온 정보에, 방어도를 얻을 캐릭터를 넣을 것이다.
        //그 캐릭터의 방어도를 증가시킨다.

        target.stat.block += amount;


    }

    public void Dedicate(Character user, Character target, int amount)  //헌신
    {
        if(perkManager.perkDict[user.perk] != null) // 코드 중복 제거한다고 합치지 말 것
        {
            perkManager.perkDict[user.perk](ref amount);
        }
        else
        {
            Debug.Log("User : " + user + "'s perk is not initialized.");
        }
        //파라미터로 들어온 정보에, 스테미나를 얻을 캐릭터를 넣을 것이다.
        //그 캐릭터의 스테미나를 증가시킨다.스테미나는 최댓값이 없다.

        target.stat.stamina += amount;
    }

    public void AddStatusEffect(Character target, StatusEffectID statusEffectID, int remainTurn)
    {
        target.AddStatusEffect(statusEffectID, remainTurn);
    }

    public List<Character> GetAllEnemies()
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
        return enemies;
    }

    public List<Character> GetAllPlayers()
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
        return players;
    }
}
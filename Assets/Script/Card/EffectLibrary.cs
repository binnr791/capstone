using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLibrary : MonoBehaviour
{

    //캐릭터 공격
    public void Attack(Character user, Character target, int amount) // user는 사용 캐릭터이고, target은 대상 캐릭터다.
    {
        target.stat.hp -= amount;
        // 사망 판정은 effectlibrary에서 하지 않을 겁니다.
        

    }


    //캐릭터 힐
    public void Heal(Character user, Character target, int amount)
    {
        
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
        //파라미터로 들어온 정보에, 방어도를 얻을 캐릭터를 넣을 것이다.
        //그 캐릭터의 방어도를 증가시킨다.

        target.stat.block += amount;


    }

    public void Dedicate(Character user, Character target, int amount)  //헌신
    {

        //파라미터로 들어온 정보에, 스테미나를 얻을 캐릭터를 넣을 것이다.
        //그 캐릭터의 스테미나를 증가시킨다.스테미나는 최댓값이 없다.

        target.stat.stamina += amount;


    }

}
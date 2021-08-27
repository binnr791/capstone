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
        // int health;
        // int heal;
        // bool all;
        // int tmp;

        // if (all == true) //모든 아군에게
        // {
        //     health = health + heal; 
        //     if (health <= 0)
        //     {
        //         health = 0;
        //     }

        // }
        // else  //지정 대상에게
        // {
        //     health = health + heal;
        //     if (health <= 0)
        //     {
        //         health = 0; 
        //     }
        // }

        //캐릭이 죽었으면 힐이 되지 않게하기..

        //최대체력 이상 힐되지 않게 하기.
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

    }

    public void Dedicate(Character user, Character target, int amount)  //헌신
    {

        //파라미터로 들어온 정보에, 스테미나를 얻을 캐릭터를 넣을 것이다.
        //그 캐릭터의 스테미나를 증가시킨다.스테미나는 최댓값이 없다.

    }

    // public void Die() //죽은 캐릭 표현?
    // {
    //     // int health;
    //     // bool isDie;

    //     //     if (health <= 0) 
    //     //     {
    //     //         isDie = true;
    //     //         return true;
    //     //     }
    //     //     return false;
        
    // }
}
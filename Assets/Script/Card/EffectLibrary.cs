using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectLibrary : MonoBehaviour
{
    [SerializeField] Deck deck;

    //캐릭터 공격
    public void Attack(Character user, List<Character> targets, int amount) // user는 사용 캐릭터이고, target은 대상 캐릭터다.
    {
        // bool all을 빼고 List로 target을 줄 것임.

        // int health; 주석 풀고 알아서 개발하기
        // int damage;
        // bool all;

        // if (all == true) 
        // {
        //     //모든 적군에게
        //     health = health - damage;
        // }
        // else
        //     //지정 대상에게
        //     health = health - damage;
    }

    //캐릭터 힐
    public void Heal(Character user, List<Character> targets, int amount)
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
        //덱에서 더 이상 드로우 할 수 없으면 묘지에서 가져오기
        deck.DrawCard(); // 이건 제가 했습니다.
    }

    public void Bury() //묘지 함수 
    {

        //묘지에 가상의 카드 넣기 함수가 있다고 생각하고, 호출한다.
        //패의 카드는 반드시 제거되어야한다.

    }

    public void Recycle()  //재활용
    {

        //구현 방법이 여러가지이다. 따로 함수를 만들지 않고,
        //묘지로 가기 함수에서 분기를 만들어서 구현해도 된다.

    }

    public void Armor(Character user, List<Character> targets, int amount) //방어도 증가
    {
        //파라미터로 들어온 정보에, 방어도를 얻을 캐릭터를 넣을 것이다.
        //그 캐릭터의 방어도를 증가시킨다.

    }

    public void Dedicate(Character user, List<Character> targets, int amount)  //헌신
    {

        //파라미터로 들어온 정보에, 스테미나를 얻을 캐릭터를 넣을 것이다.
        //그 캐릭터의 스테미나를 증가시킨다.스테미나는 최댓값이 없다.

    }

    public void Die() //죽은 캐릭 표현?
    {
        // int health;
        // bool isDie;

        //     if (health <= 0) 
        //     {
        //         isDie = true;
        //         return true;
        //     }
        //     return false;
        
    }
}
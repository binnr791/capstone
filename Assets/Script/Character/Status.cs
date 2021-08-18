using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int maxhp;
    public int hp;
    public int block; // 방어도
    public int stamina;
    public int baseSpeed;
    public int nowSpeed;
    
    void Start()
    {
        baseSpeed = 50;
        nowSpeed = 50;
    }
}

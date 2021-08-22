using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Status : MonoBehaviour
{
    public int maxhp;
    public int hp;
    public int block; // 방어도
    public int stamina;
    public int baseSpeed;
    public int nowSpeed;
    public enum Faction {Player,Enemy} ;
    public Faction faction;
    public int index;
}

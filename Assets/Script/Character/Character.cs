using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Status stat;
    public Faction faction;
}

public enum Faction // 아군 적 구분
{
    Ally, Enemy
}
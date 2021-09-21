using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveGameData
{
    // metadata
    public float version;
    public int seed;

    // character & deck
    public int gold;
    public List<CharacterSaveData> characters;
    public List<int> deck;

    // dungeon
    public int dungeonIndex;
}

[System.Serializable]
public struct CharacterSaveData
{
    public int id;
    public int maxHealth;
    public int health;
}
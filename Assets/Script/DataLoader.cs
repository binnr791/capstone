using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-10)]
public class DataLoader : MonoBehaviour
{
    public static DataLoader instance;
    [SerializeField] RectTransform field;

    public List<StatInspector> characterStats;
    public List<StatInspector> enemyStats;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }        
    }

    public List<GameObject> CreateField()
    {
        GameObject characterPrefab = Resources.Load<GameObject>("Prefab/Character/Ally_B");
        GameObject enemyPrefab = Resources.Load<GameObject>("Prefab/Character/enemy");
        List<GameObject> result = new List<GameObject>();

        int charIndex = 0;

        // ////////////////////////// character ////////////////////////
        for(int i = 0; i < characterStats.Count; i++)
        {
            GameObject characterObj = Instantiate(characterPrefab);
            characterObj.GetComponent<RectTransform>().SetParent(field);
            characterObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-400, 300 - (i * 350));
            
            AssignStatus(characterObj, characterStats[i]);
            characterObj.GetComponent<Image>().sprite = characterStats[i].charSprite;
            characterObj.GetComponent<Character>().charName = characterStats[i].charName;
            characterObj.GetComponent<Character>().faction = Faction.Player;
            characterObj.GetComponent<Character>().index = charIndex++;
            
            result.Add(characterObj);
        }

        // ////////////////////////// enemy ////////////////////////

        for(int i = 0; i < enemyStats.Count; i++)
        {
            GameObject characterObj = Instantiate(enemyPrefab);
            characterObj.GetComponent<RectTransform>().SetParent(field);
            characterObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(400, 300 - (i * 350));
            
            AssignStatus(characterObj, enemyStats[i]);
            characterObj.GetComponent<Image>().sprite = enemyStats[i].charSprite;
            characterObj.GetComponent<Character>().charName = enemyStats[i].charName;
            characterObj.GetComponent<Character>().faction = Faction.Enemy;
            characterObj.GetComponent<Character>().index = charIndex++;
            
            result.Add(characterObj);
        }

        return result;
    }

    private void AssignStatus(GameObject character, StatInspector newStat)
    {
        Character charToAssign = character.GetComponent<Character>();
        charToAssign.stat.maxhp = newStat.maxhp;
        charToAssign.stat.hp = newStat.hp;
        charToAssign.stat.baseSpeed = newStat.baseSpeed;
    }
}

[System.Serializable]
public struct StatInspector
{
    public int maxhp;
    public int hp;
    public int baseSpeed;
    public Sprite charSprite;
    public string charName;
}
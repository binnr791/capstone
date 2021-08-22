using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        List<GameObject> result = new List<GameObject>();

        // characterStats = new List<StatInspector>();
        // enemyStats = new List<StatInspector>();

        int charIndex = 0;

        // ////////////////////////// character ////////////////////////
        for(int i = 0; i < characterStats.Count; i++)
        {
            GameObject characterObj = Instantiate(characterPrefab);
            characterObj.GetComponent<RectTransform>().SetParent(field);
            characterObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(-400, 300 - (i * 350));
            
            AssignStatus(characterObj, characterStats[i]);
            characterObj.GetComponent<Status>().faction = Status.Faction.Player;
            characterObj.GetComponent<Status>().index = charIndex++;
            
            result.Add(characterObj);
        }

        // ////////////////////////// enemy ////////////////////////

        for(int i = 0; i < enemyStats.Count; i++)
        {
            GameObject characterObj = Instantiate(characterPrefab);
            characterObj.GetComponent<RectTransform>().SetParent(field);
            characterObj.GetComponent<RectTransform>().anchoredPosition = new Vector2(400, 300 - (i * 350));
            
            AssignStatus(characterObj, enemyStats[i]);
            characterObj.GetComponent<Status>().faction = Status.Faction.Enemy;
            characterObj.GetComponent<Status>().index = charIndex++;
            
            result.Add(characterObj);
        }

        return result;
    }

    private void AssignStatus(GameObject character, StatInspector newStat)
    {
        Status stat = character.GetComponent<Status>();
        stat.maxhp = newStat.maxhp;
        stat.hp = newStat.hp;
        stat.baseSpeed = newStat.baseSpeed;
    }
}

[System.Serializable]
public struct StatInspector
{
    public int maxhp;
    public int hp;
    public int baseSpeed;
}
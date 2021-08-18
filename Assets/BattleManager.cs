using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public GameObject[] objs;
    public int turnNum;
    public List<GameObject> turnList = new List<GameObject>();
    bool drawOrGetStamina;
    bool useCard;
    bool staminaGet = false;
    bool cardDraw = false;
    bool notChoice = true;
    // Start is called before the first frame update
    void Start()
    {
        //turn reset
        turnNum = -1;

        //Make Player Character visible
        objs = GameObject.FindGameObjectsWithTag("J_Player");
        for (int i = 0; i < objs.Length; i++)
        {
            objs[i].GetComponent<Renderer>().enabled = true;
        }
        // Create Enemy
        Instantiate(Resources.Load("Prefab/J_Enemy1"), new Vector3(8,2,0),Quaternion.identity);
        Instantiate(Resources.Load("Prefab/J_Enemy2"), new Vector3(8,-2,0),Quaternion.identity);
        
        //Player Character & Enemy Character List
        GameObject[] allCharacters = GameObject.FindGameObjectsWithTag("J_Player");
        foreach (GameObject character in allCharacters) {
            turnList.Add (character);
        }
        allCharacters = GameObject.FindGameObjectsWithTag("J_Enemy");
        foreach (GameObject character in allCharacters) {
            turnList.Add (character);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(turnNum == -1)
        {
            //nowspeed set Based on baseSpeed -5 to +5
            foreach (GameObject character in turnList)
            {
                character.GetComponent<Status>().nowSpeed = character.GetComponent<Status>().baseSpeed + Random.Range(-5,6);
                //Debug.Log(character.GetComponent<Status>().nowSpeed);
            }
            //Sorting characters Speed
            if (turnList.Count > 0) {
                turnList.Sort(delegate(GameObject b, GameObject a) {
                    return (a.GetComponent<Status>().nowSpeed).CompareTo(b.GetComponent<Status>().nowSpeed);
                });
            }
            /*
            // Sorting check
            Debug.Log("Sorting Finish");
            foreach (GameObject character in turnList)
            {
                Debug.Log(character.GetComponent<Status>().nowSpeed);
            }
            */
            drawOrGetStamina = true;
            useCard = true;
            turnNum ++;
        }
        else if(turnNum >= turnList.Count)
        {
            turnNum = -1;
        }
        else if(turnList[turnNum].CompareTag("J_Enemy"))
        {
            //Enemy Act
            Debug.Log("turn"+turnNum+" Enemy Act");
            turnNum++;
        }
        else if(turnList[turnNum].CompareTag("J_Player"))
        {
            
            if (drawOrGetStamina)
            {
                //draw or getstamina
            }
            else if (useCard)
            {

            }
            else // turn end
            {
                drawOrGetStamina = true;
                useCard = true;
                turnNum++;
            }

            Debug.Log("turn"+turnNum+" Player Act");
        }
    }

    void creatChoice()
    {
        if(notChoice)
        {
            //Instantiate(Resources.Load("Prefab/J_Enemy1"), new Vector3(8,2,0),Quaternion.identity);
            notChoice = false;
        }
    }
}


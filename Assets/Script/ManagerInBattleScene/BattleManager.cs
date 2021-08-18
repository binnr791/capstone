using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public GameObject[] objs;
    public int turnNum;
    public List<GameObject> turnList = new List<GameObject>();
    bool drawOrGetStamina;
    bool useCard;
    bool staminaGet = false;
    bool cardDraw = false;
    bool notChoice = true;

    // 캐릭터 레퍼런스를 가져와서 수정하는 걸로 구현하기
    public List<Character> character;


    public delegate void phase();
    public phase nextPhase;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        
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
    // void Update()
    // {
    //     if(turnNum == -1)
    //     {
    //         //nowspeed set Based on baseSpeed -5 to +5
    //         foreach (GameObject character in turnList)
    //         {
    //             character.GetComponent<Status>().nowSpeed = character.GetComponent<Status>().baseSpeed + Random.Range(-5,6);
    //             //Debug.Log(character.GetComponent<Status>().nowSpeed);
    //         }
    //         //Sorting characters Speed
    //         if (turnList.Count > 0) {
    //             turnList.Sort(delegate(GameObject b, GameObject a) {
    //                 return (a.GetComponent<Status>().nowSpeed).CompareTo(b.GetComponent<Status>().nowSpeed);
    //             });
    //         }
    //         /*
    //         // Sorting check
    //         Debug.Log("Sorting Finish");
    //         foreach (GameObject character in turnList)
    //         {
    //             Debug.Log(character.GetComponent<Status>().nowSpeed);
    //         }
    //         */
    //         drawOrGetStamina = true;
    //         useCard = true;
    //         turnNum ++;
    //     }
    //     else if(turnNum >= turnList.Count)
    //     {
    //         turnNum = -1;
    //     }
    //     else if(turnList[turnNum].CompareTag("J_Enemy"))
    //     {
    //         //Enemy Act
    //         Debug.Log("turn"+turnNum+" Enemy Act");
    //         turnNum++;
    //     }
    //     else if(turnList[turnNum].CompareTag("J_Player"))
    //     {
            
    //         if (drawOrGetStamina)
    //         {
    //             //draw or getstamina
    //         }
    //         else if (useCard)
    //         {

    //         }
    //         else // turn end
    //         {
    //             drawOrGetStamina = true;
    //             useCard = true;
    //             turnNum++;
    //         }

    //         Debug.Log("turn"+turnNum+" Player Act");
    //     }
    // }

    public void StartTurnPhase()
    {

    }
    
    public void GainResourcePhase() // 자원 획득 단계
    {
        Debug.Log("Gain Resource Phase");
        
        // uimanager will decide when to change to next phase
    }

    public void UseCardPhase() // 카드 사용 단계
    {
        Debug.Log("Use Card Phase");
        // uimanager will decide when to change to next phase
    }

    public void PassTurnPhase() // 참고용 구현, 이 주석 라인은 읽고 삭제하기
    {
        if(turnNum >= turnList.Count)
        {
            nextPhase += NextCyclePhase;
        }
        else
        {
            turnNum += 1;
            
            // if ally
            nextPhase += GainResourcePhase;
            // if enemy
            nextPhase += EnemyActPhase;
            // implement something
        }
        nextPhase();
    }
    
    public void EnemyActPhase()
    {
        nextPhase();
    }

    public void NextCyclePhase()
    {

    }

    public void NextPhase() // 강제로 페이즈를 넘기는 기능, 주로 UI 매니저가 호출함
    {
        nextPhase();
    }

    public void ChooseGainStamina() // not phase
    {
        nextPhase();
    }

    public void ChooseDrawCard() // not phase
    {
        // deck.DrawCard();
        // deck.DrawCard();
        nextPhase();
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


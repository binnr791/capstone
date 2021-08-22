using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public GameObject[] objs;
    public GameObject drawButton;
    public GameObject getStamina;
    public int turnNum;
    public List<GameObject> turnList = new List<GameObject>();
    public bool playerAct;

    // 캐릭터 레퍼런스를 가져와서 수정하는 걸로 구현하기
    public List<Character> character;

    public Card usingCard;

    public delegate void phase();
    public phase nextPhase;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        drawButton = GameObject.Find("DrawButton");
        getStamina = GameObject.Find("GetStamina");
        drawButton.SetActive(false);
        getStamina.SetActive(false);
        playerAct = false;
    }

    public void BattleStart()
    {
        //turn reset
        turnNum = -1;

        //Load Player Characters
        objs = GameObject.FindGameObjectsWithTag("Character");
        for (int i = 0; i < objs.Length; i++)
        {
            //Make Player Character visible
            objs[i].GetComponent<Renderer>().enabled = true;
            //Add Player Character to List
            turnList.Add (objs[i]);
        }
        // Create Enemy & Add Enemy Character to List
        turnList.Add((GameObject)Instantiate(Resources.Load("Prefab/Character/J_Enemy1"), new Vector3(8,2,0),Quaternion.identity));
        turnList.Add((GameObject)Instantiate(Resources.Load("Prefab/Character/J_Enemy2"), new Vector3(8,-2,0),Quaternion.identity));
        Debug.Log(turnList.Count);
        TurnAssignment();
    }

    void TurnAssignment() //Turn Mix
    {
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
        turnNum++;
        StartTurnPhase();
    }

    void StartTurnPhase()
    {
        //Turn Reset
        if(turnNum >= turnList.Count)
        {
            turnNum = -1;
            TurnAssignment();
        }
        //Player Turn
        else if(turnList[turnNum].GetComponent<Status>().faction == Status.Faction.Player)
        {
            Debug.Log("Player Action");
            DrawTurn();
        }//Enemy Turn
        else if(turnList[turnNum].GetComponent<Status>().faction == Status.Faction.Enemy)
        {
            Debug.Log("Enemy Action");
            StartCoroutine(EnemyActPhase());
            StartTurnPhase();
        }
        else
        {
            Debug.Log("Error");
        }
    }
    // Enemy Action
    IEnumerator EnemyActPhase()
    {
        turnNum++;
        yield return null;
    }
    
    public void DrawTurn()
    {
        drawButton.SetActive(true);
        getStamina.SetActive(true);
    }

    public IEnumerator Draw()
    {
        Debug.Log("Draw Card");
        yield return null;
        UseCardPhase();
    }

    public IEnumerator GainResourcePhase()
    {
        Debug.Log("Get Stamina");
        yield return null;
        UseCardPhase();
    }

    public void UseCardPhase()  // 카드 사용 가능 설정
    {
        playerAct = true;
        getStamina.SetActive(false);
        drawButton.SetActive(false);
        Debug.Log("Use Card Phase");
        // uimanager will decide when to change to next phase
    }

    IEnumerator PlayTurn()  // 카드사용단계
    {
        Debug.Log("Play Action");
        yield return null;
    }

    public IEnumerator EndTurn()
    {
        playerAct = false;
        Debug.Log("Turn End");
        turnNum++;
        yield return null;
        StartTurnPhase();
    }

    public void TryUsingCard()
    {
        if(HasCardProperty(usingCard, CardProperty.ChooseTarget))
        {
            BattleUIManager.instance.EnableChooseResource();
            nextPhase += UserInputStep;
            NextPhase();
        }
        else
        {
            nextPhase += CardEffectStep;
            NextPhase();
        }
    }

    public void UserInputStep()
    {
        bool userInput = true; // or false
        //BattleUIManager.instance.
        // if(userInput)
        // {
        //     nextPhase += CardEffectStep;
        // }
        // else // cancel using card
        // {
        //     nextPhase += UseCardPhase;
        // }
        // uimanager will call nextphase
    }

    public void CardEffectStep()
    {
        //BattleUIManager.instance.
        usingCard.effectInfo.effect();
        nextPhase += UseCardPhase;
        NextPhase();
    }

    private bool HasCardProperty(Card card, CardProperty property) // 카드에 property가 있는 지 확인하는 함수
    {
        if(card.effectInfo.properties != null)
        {
            for(int n = 0; n < card.effectInfo.properties.Count; n++)
            {
                if(card.effectInfo.properties[n] == property)
                {
                    return true;
                }
            }
        }
        return false;
    }

    // public void PassTurnPhase() // 참고용 구현, 이 주석 라인은 읽고 삭제하기
    // {
    //     if(turnNum >= turnList.Count)
    //     {
    //         nextPhase += NextCyclePhase;
    //     }
    //     else
    //     {
    //         turnNum += 1;
            
    //         // if ally
    //         // nextPhase += GainResourcePhase;
    //         // // if enemy
    //         // nextPhase += EnemyActPhase;
    //         // implement something
    //     }
    //     nextPhase();
    // }

    // public void NextCyclePhase()
    // {

    // }

    public void NextPhase() // 강제로 페이즈를 넘기는 기능, 주로 UI 매니저가 호출함
    {
        nextPhase();
    }

    // public void ChooseGainStamina() // not phase
    // {
    //     nextPhase();
    // }

    // public void ChooseDrawCard() // not phase
    // {
    //     // deck.DrawCard();
    //     // deck.DrawCard();
    //     nextPhase();
    // }


    // void creatChoice()
    // {
    //     // if(notChoice)
    //     // {
    //     //     //Instantiate(Resources.Load("Prefab/J_Enemy1"), new Vector3(8,2,0),Quaternion.identity);
    //     //     notChoice = false;
    //     // }
    // }
}


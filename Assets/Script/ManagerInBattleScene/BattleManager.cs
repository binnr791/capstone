using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public static BattleManager instance;

    public GameObject[] objs;
    public GameObject drawButton; // use inspector
    public GameObject getStamina;
    public int turnNum;
    public List<GameObject> turnList = new List<GameObject>();
    public bool playerAct;
    public bool userInput;
    public int targetCharacter = -1; // -1 case will cause target required error
    public int userCharacter = -1;

    // 캐릭터 레퍼런스를 가져와서 수정하는 걸로 구현하기
    public List<Character> charactersInfo;

    public Card usingCard;

    public delegate void phase();
    public phase nextPhase;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        drawButton.SetActive(false);
        getStamina.SetActive(false);
        playerAct = false;

        BattleStart();
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

        List<GameObject> characters = DataLoader.instance.CreateField();
        for(int i = 0; i < characters.Count; i++)
        {
            turnList.Add(characters[i]);
            charactersInfo.Add(characters[i].GetComponent<Character>());
        }

        Debug.Log("Current Num of Characters : " + turnList.Count);
        TurnAssignment();
    }

    void TurnAssignment() //Turn Mix
    {
        foreach (GameObject character in turnList)
        {
            character.GetComponent<Character>().stat.nowSpeed = character.GetComponent<Character>().stat.baseSpeed + Random.Range(-5,6);
            //Debug.Log(character.GetComponent<Character>().stat.nowSpeed);
        }
        //Sorting characters Speed
        if (turnList.Count > 0) {
            turnList.Sort(delegate(GameObject b, GameObject a) {
                return (a.GetComponent<Character>().stat.nowSpeed).CompareTo(b.GetComponent<Character>().stat.nowSpeed);
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
        else if(turnList[turnNum].GetComponent<Character>().faction == Faction.Player)
        {
            Debug.Log("Player Action");
            DrawTurn();

        }//Enemy Turn
        else if(turnList[turnNum].GetComponent<Character>().faction == Faction.Enemy)
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
        if(usingCard.HasCardProperty(CardProperty.ChooseTarget))
        {
            BattleUIManager.instance.EnableChooseResource();
            nextPhase = UserInputStep;
            NextPhase();
        }
        else
        {
            nextPhase = CardEffectStep;
            NextPhase();
        }
    }

    public void UserInputStep()
    {
        bool userInput = true; // or false
        //BattleUIManager.instance.
        // if(userInput)
        // {
        //     nextPhase = CardEffectStep;
        // }
        // else // cancel using card
        // {
        //     nextPhase = UseCardPhase;
        // }
        // uimanager will call nextphase
    }

    public void CardEffectStep()
    {
        //BattleUIManager.instance.
        usingCard.effectInfo.effect();
        Destroy(usingCard.gameObject); // must add card to grave, fix it!
        nextPhase = UseCardPhase;
        NextPhase();
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
    //         // nextPhase = GainResourcePhase;
    //         // // if enemy
    //         // nextPhase = EnemyActPhase;
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

    public void ChooseGainStamina() // not phase
    {
        turnList[turnNum].GetComponent<Character>().stat.stamina += 3;
        UseCardPhase();
    }

    public void ChooseDrawCard() // not phase
    {
        //StartCoroutine("GainResourcePhase");
        CardManager.instance.DrawCard();
        CardManager.instance.DrawCard();
        UseCardPhase();
    }


    // void creatChoice()
    // {
    //     // if(notChoice)
    //     // {
    //     //     //Instantiate(Resources.Load("Prefab/J_Enemy1"), new Vector3(8,2,0),Quaternion.identity);
    //     //     notChoice = false;
    //     // }
    // }

    public Character GetTargetCharacter()
    {
        return charactersInfo[targetCharacter];
    }

    public Character GetUserCharacter()
    {
        return charactersInfo[userCharacter];
    }

    public void CancelUsingCard()
    {
        usingCard.GetComponent<RectTransform>().SetParent(CardManager.instance.HandTransform);
        int index = usingCard.GetComponent<Cardpop>().index;
        usingCard.GetComponent<RectTransform>().SetSiblingIndex(index);
        usingCard.gameObject.SetActive(true);
        userCharacter = -1;
        targetCharacter = -1;
        userInput = false;
        usingCard = null;
    }
}


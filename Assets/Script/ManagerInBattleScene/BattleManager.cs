using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
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

    public int initialDrawCards = 4;
    
    private EnemyAct enemyAct;

    public Character currentTurnChar;

    // 캐릭터 레퍼런스를 가져와서 수정하는 걸로 구현하기
    public List<Character> charactersInfo;

    public Card usingCard;

    public delegate void phase();
    public phase nextPhase;

    void Start()
    {
        enemyAct = GetComponent<EnemyAct>();

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
        List<GameObject> characters = DataLoader.instance.CreateField();
        for(int i = 0; i < characters.Count; i++)
        {
            turnList.Add(characters[i]);
            charactersInfo.Add(characters[i].GetComponent<Character>());
        }

        Debug.Log("Current Num of Characters : " + turnList.Count);

        for(int i = 0; i < characters.Count; i++)
        {
            if(characters[i].GetComponent<Character>().faction == Faction.Player)
            {
                characters[i].GetComponent<Character>().battleStart(); // 스테미나 초기화, 전투 시작시 효과 발동
            }
        }
        for(int i = 0; i < initialDrawCards; i++)
        {
            CardManager.instance.DrawCard();
        }

        StartCyclePhase();
    }

    public void StartCyclePhase()
    {
        turnNum = -1;
        TurnAssignment();
        StartTurnPhase();
    }

    void TurnAssignment() //Turn Mix
    {
        foreach (GameObject character in turnList)
        {
            character.GetComponent<Character>().stat.nowSpeed = character.GetComponent<Character>().stat.baseSpeed + Random.Range(-5,6);
        }
        //Sorting characters Speed
        if (turnList.Count > 0) {
            turnList.Sort(delegate(GameObject b, GameObject a) {
                return (a.GetComponent<Character>().stat.nowSpeed).CompareTo(b.GetComponent<Character>().stat.nowSpeed);
            });
        }
        turnNum++;
    }

    void StartTurnPhase()
    {
        //Player Turn
        if(turnList[turnNum].GetComponent<Character>().faction == Faction.Player)
        {
            turnList[turnNum].GetComponent<Character>().nowturn.color = new Color(1, 71/255f, 83/255f, 120/255f);
            Debug.Log("Player Turn Start : " + turnList[turnNum].GetComponent<Character>().charName);
            CardManager.instance.UpdateAvailableHand();
            DrawTurn();

        }//Enemy Turn
        else if(turnList[turnNum].GetComponent<Character>().faction == Faction.Enemy)
        {
            Debug.Log("Enemy Turn Start : " + turnList[turnNum].GetComponent<Character>().charName);
            //turnList[turnNum].GetComponent<Character>().nowturn.color = new Color(1, 71/255f,83 / 255f, 120/255f);
            //turnList[turnNum-1].GetComponent<Character>().nowturn.color = new Color(165 / 255f, 1, 108 / 255f, 70/255f);
            CardManager.instance.UpdateAvailableHand();
            EnemyActPhase();
        }
        else
        {
            Debug.Log("Error");
        }
    }
    // Enemy Action

    public void EnemyActPhase()
    {
        enemyAct.GetRandomEnemyAction() ();
        CheckGameState();
        TurnEnd();
    }
    
    public void DrawTurn()
    {
        drawButton.SetActive(true);
        getStamina.SetActive(true);
    }


    public void UseCardPhase()  // 카드 사용 가능 설정
    {
        playerAct = true;
        getStamina.SetActive(false);
        drawButton.SetActive(false);
        Debug.Log("Use Card Phase");
        // uimanager will decide when to change to next phase
    }

    public void TurnEnd()
    {
        if(GetCurrentTurnChar().faction == Faction.Player)
        {
            GetCurrentTurnChar().TurnEndEvent();
            playerAct = false;
            GetCurrentTurnChar().nowturn.color = new Color(165 / 255f, 1, 108 / 255f, 70/ 255f);
            Debug.Log("Player Turn End");
        }
        else if(GetCurrentTurnChar().faction == Faction.Enemy)
        {
            Debug.Log("Enemy Turn End");
        }
        else
        {
            Debug.LogError("Not implemented turn end of new faction char!");
        }
        turnNum++;
        if (turnNum >= turnList.Count)
        {
            StartCyclePhase();
        }
        else
        {
            StartTurnPhase();
        }
    }
    
    public void ChooseGainStamina() // not phase
    {
        turnList[turnNum].GetComponent<Character>().stat.stamina += 3;
        CardManager.instance.UpdateAvailableHand();
        UseCardPhase();
    }

    public void ChooseDrawCard() // not phase
    {
        CardManager.instance.DrawCard();
        CardManager.instance.DrawCard();
        UseCardPhase();
    }

    public void CheckStaminaEnough()
    {
        if (turnList[turnNum].GetComponent<Character>().stat.stamina >= usingCard.cost)
        {
            ///CardEffectStep();
        }
        else
        {
            Debug.Log("Not Enough Stamina");
            CancelUsingCard();
        }
    }

    public void UserInputStep()
    {
        userInput = true; // or false
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


    public void CardEffectStep()
    {
        turnList[turnNum].GetComponent<Character>().stat.stamina -= usingCard.cost;
        usingCard.effectInfo.effect();
        usingCard.gameObject.SetActive(true);
        usingCard.GetComponent<Cardpop>().ExplictlyEndDrag();
        CardManager.instance.HandleUsedCard(usingCard);

        nextPhase = UseCardPhase;
        CheckGameState(); // if false, battle will be interrupted.
        NextPhase();
    }

    public void CheckGameState() // 게임이 종료될지 체크함
    {
        CheckCharDeath();
        if(IsGameLose())
        {
            nextPhase = LoseGame; // this phase will interrupt flow of game loop.
        }
        else if(IsGameWin())
        {
            nextPhase = WinGame; // this phase will interrupt flow of game loop.
        }
        else
        {
            // do nothing
        }
    }

    public bool CheckCharDeath()
    {
        bool isDead = false;
        for(int i = 0; i < turnList.Count; i++)
        {
            if(turnList[i].GetComponent<Character>().stat.hp <= 0) // 죽음 판정 실행
            {
                isDead = true;
                GameObject deadChar = turnList[i];
                deadChar.SetActive(false);
                //charactersInfo.Remove(deadChar.GetComponent<Character>());
                
                Debug.Log("Current Number of Characters : " + turnList.Count);
                Debug.Log("now turn " + turnNum);

                if(i <= turnNum) // 앞순서의 적이 죽었을때 순서를 1씩 당겨줌, 이 사이클에서 먼저 턴을 가졌었던 캐릭터가 죽는 경우
                {
                    if(i == turnNum) // 턴을 가진 캐릭터가 죽을 때의 처리
                    {
                        nextPhase = SkipTurnPhase;
                    }
                    turnNum--;
                    i--;
                }

                turnList.Remove(deadChar);
                charactersInfo.RemoveAt(deadChar.GetComponent<Character>().index);
                
                // 캐릭터 인덱스 갱신 필요
                int newCharIndex = 0;
                for(int c = 0; c < charactersInfo.Count; c++)
                {
                    charactersInfo[c].index = newCharIndex;
                    newCharIndex++;
                }
            }
        }
        return isDead;
    }

    public bool IsGameWin()
    {
        for(int i = 0; i < turnList.Count; i++)
        {
            if(charactersInfo[i].GetComponent<Character>().faction == Faction.Enemy)
            {
                return false;
            }
        }
        return true;
    }

    public bool IsGameLose() // 동시에 모두 다 죽으면 패배로 처리
    {
        for(int i = 0; i < turnList.Count; i++)
        {
            if(turnList[i].GetComponent<Character>().faction == Faction.Player)
            {
                return false;
            }
        }
        return true;
    }

    private void WinGame()
    {
        Debug.Log("You Win");
    }

    private void LoseGame()
    {
        Debug.Log("You Lose");
    }

    // public void PassTurnPhase() // 참고용 구현, 이 주석 라인은 읽고 삭제하기
    // {
    //     turnNum += 1;
    //     if(turnNum >= turnList.Count)
    //     {
    //         nextPhase = EndCyclePhase;
    //     }
    //     else
    //     {
    //         nextPhase = StartTurnPhase;
    //     }
    //     nextPhase();
    // }

    public void SkipTurnPhase() // 참고용 구현, 이 주석 라인은 읽고 삭제하기
    {
        if(turnNum >= turnList.Count)
        {
            nextPhase = StartCyclePhase;
        }
        else
        {
            nextPhase = StartTurnPhase;
        }
        nextPhase();
    }

    public void NextPhase() // 강제로 페이즈를 넘기는 기능, 주로 UI 매니저가 호출함
    {
        nextPhase();
    }

    public Character GetTargetCharacter()
    {
        return charactersInfo[targetCharacter];
    }

    public Character GetUserCharacter()
    {
        Character currentChar = GetCurrentTurnChar();
        return currentChar;
    }

    public Character GetCurrentTurnChar()
    {
        if(turnNum < 0)
        {
            return null;
        }
        currentTurnChar = turnList[turnNum].GetComponent<Character>();
        return turnList[turnNum].GetComponent<Character>();
    }

    public void CancelUsingCard()
    {
        usingCard.GetComponent<RectTransform>().SetParent(CardManager.instance.HandTransform);
        int index = usingCard.GetComponent<Cardpop>().index;
        usingCard.GetComponent<RectTransform>().SetSiblingIndex(index);
        BattleUIManager.instance.DisableInstruction();
        usingCard.gameObject.SetActive(true);
        userCharacter = -1;
        targetCharacter = -1;
        userInput = false;
        usingCard = null;
    }


    // debug function

    public void InfiniteStamina()
    {
        for(int i = 0; i < charactersInfo.Count; i++)
        {
            charactersInfo[i].stat.stamina = 99;
        }
    }
}
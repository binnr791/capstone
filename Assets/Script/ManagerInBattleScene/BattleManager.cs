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
            turnNum++;
            StartTurnPhase();
        }
        else
        {
            Debug.Log("Error");
        }
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
        if(BattleManager.instance.playerAct)
        {
            playerAct = false;
            Debug.Log("Turn End");
            turnNum++;
            StartTurnPhase();
        }
        else
        {
            Debug.Log("Not Player Turn");
        }
    }
    
    public void ChooseGainStamina() // not phase
    {
        turnList[turnNum].GetComponent<Character>().stat.stamina += 3;
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
        usingCard.effectInfo.effect();
        Destroy(usingCard.gameObject); // must add card to grave, fix it!

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
                turnList.Remove(deadChar);
                Debug.Log("remain "+turnList.Count);
                Debug.Log("now turn "+turnNum);
                if(i < turnNum) // 앞순서의 적이 죽었을때 순서를 1씩 당겨줌
                {
                    turnNum--;
                }

                // Character deadChar = turnList[i].GetComponent<Character>(); // not use
                // charactersInfo[deadChar.index.gameObject.SetActive(false);
                // charactersInfo.RemoveAt(deadChar.index);
                // turnList.RemoveAt(i);

                if(i == turnNum) // 턴을 가진 캐릭터가 죽을 때의 처리
                {
                    turnNum--;
                    nextPhase = SkipTurnPhase;
                }

                
                // 대상 지정 영역 갱신 필요
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

    public void PassTurnPhase() // 참고용 구현, 이 주석 라인은 읽고 삭제하기
    {
        turnNum += 1;
        if(turnNum >= turnList.Count)
        {
            nextPhase = EndCyclePhase;
        }
        else
        {
            nextPhase = StartTurnPhase;
        }
        nextPhase();
    }

    public void SkipTurnPhase() // 참고용 구현, 이 주석 라인은 읽고 삭제하기
    {
        if(turnNum >= turnList.Count)
        {
            nextPhase = EndCyclePhase;
        }
        else
        {
            nextPhase = StartTurnPhase;
        }
        nextPhase();
    }

    public void EndCyclePhase()
    {

    }

    public void StartCyclePhase()
    {

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
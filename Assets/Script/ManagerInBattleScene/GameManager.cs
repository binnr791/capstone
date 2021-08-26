using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-10)] // 먼저 초기화함

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // public int health1 = 100;
    // public int stamina1;
    
    // public int health2 = 100;
    // public int stamina2;

    // public int enemy_health1;
    // public int enemy_stam1;

    // public int enemy_health2;
    // public int enemy_stam2;

    public int dungeon_number;
    public int cycle_number = 0;
    
    public Text UIStage;
    public Text UIcycle;
    // Start is called before the first frame update

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // bool battleStart = false;
    // Start is called before the first frame update

    // Update is called once per frame
    // void Update()
    // {
    //     if (battleStart == false&&Input.GetKeyDown(KeyCode.Space)) // 임시로 스페이스바 눌러서 전투 시작
    //     {
    //         battleStart = true;
    //         Debug.Log("Space");
    //         Instantiate(Resources.Load("Prefab/BattleManager"), new Vector3(0,0,0),Quaternion.identity);
    //     }

    //     if (Input.GetMouseButtonDown(0)) // 임시로 클릭을 사이클 엔드로 침
    //     {
    //         cycle_number += 1;

    //         UIcycle.text = "cycle " + cycle_number;
    //     }
    // }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int health1 = 100;
    public int stamina1;
    
    public int health2 = 100;
    public int stamina2;

    public int enemy_health1;
    public int enemy_stam1;

    public int enemy_health2;
    public int enemy_stam2;

    public int dungeon_number;
    public int cycle_number = 0;
    
    public Text UIStage;
    public Text UIcycle;
    // Start is called before the first frame update


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 임시로 클릭을 사이클 엔드로 침
        {
            cycle_number += 1;

            UIcycle.text = "cycle " + cycle_number;
        }

    }
}

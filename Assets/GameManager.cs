using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    bool battleStart = false;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(Resources.Load("Prefab/J_Cha1"), new Vector3(-8,2,0),Quaternion.identity);
        Instantiate(Resources.Load("Prefab/J_Cha2"), new Vector3(-8,-2,0),Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (battleStart == false&&Input.GetKeyDown(KeyCode.Space))
        {
            battleStart = true;
            Debug.Log("Space");
            Instantiate(Resources.Load("Prefab/BattleManager"), new Vector3(0,0,0),Quaternion.identity);
        }
    }
}

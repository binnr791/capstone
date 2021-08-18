using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int hp;
    public int stamina;
    public int baseSpeed;
    public int nowSpeed;
    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = 50;
        nowSpeed = 50;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

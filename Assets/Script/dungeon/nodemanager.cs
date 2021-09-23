using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class nodemanager : MonoBehaviour
{
    public static nodemanager instance;

    public int charlocation;

    void Start()
    {
        // charlocation = 시작위치;
    }

    public void IsCharOn() // 현재 캐릭터가 위치한 노드 표시
    {

    }
    public void nodestatus() // 이미 탐사한 노드인지 아닌지
    {
        if(node.status = true)
        {
            node.SetActive(true);
        }
        else
        {
            node.SetActive(false);
        }
    }
    public void nodeevnet() //노드 이벤트
    {
        if(node.eventstatus = trap)
        {

        }
        if(node.eventstatus = cardgain)
        {

        }
        if (node.eventstatus = cardlost)
        {

        }
        if (node.eventstatus = cardchange)
        {

        }
        if (node.eventstatus = heal)
        {

        }
        if(node.eventstatus = battle)
        {

        }
    }

    void Update()
    {
        
    }
}

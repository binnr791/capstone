using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NodeManager : MonoBehaviour
{
    public static NodeManager instance;

    public int charLocation;

    public Node node;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        // charlocation = 시작위치;
    }

    public void IsCharOn() // 현재 캐릭터가 위치한 노드 표시
    {

    }
    public void NodeStatus() // 이미 탐사한 노드인지 아닌지
    {
        // if(node.status == true)
        // {
        //     node.SetActive(true);
        // }
        // else
        // {
        //     node.SetActive(false);
        // }
    }
    public void NodeEvent() //노드 이벤트
    {
        // if(node.eventstatus == cardGain)
        // {

        // }
        // if (node.eventstatus == cardLost)
        // {

        // }
        // if (node.eventstatus == cardChange)
        // {

        // }
        // if (node.eventstatus == heal)
        // {

        // }
        // if(node.eventstatus = battle)
        // {

        // }
    }
}
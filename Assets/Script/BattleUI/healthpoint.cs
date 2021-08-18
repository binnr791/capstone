using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class healthpoint : MonoBehaviour
{
    public Slider hpBar;
   // private GameManager hp; 체력 레퍼런스 가져오기
    public Text point;
    // Start is called before the first frame update
    
    void Start()
    {
        // = gameObject.GetComponent<GameManager>(); 체력 레퍼런스 가져오기
        
    }

    // Update is called once per frame
    void Update()
    {
        // hpBar.value = (float) GameManager.instance.health1 / (float) 100; //체력 레퍼런스 가져오기

        //point.text =  + " /100"; //체력 레퍼런스 가져오기
    }
}

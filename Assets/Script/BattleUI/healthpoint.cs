using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class healthpoint : MonoBehaviour
{
    [SerializeField] private Status status;
    [SerializeField] private Slider HealthBar;
   // private GameManager hp; 체력 레퍼런스 가져오기
    public Text hppoint;
    public Text maxpoint;
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        HealthBar.value = ((float) status.hp / (float) status.maxhp) * 100 ; //체력 레퍼런스 가져오기


        maxpoint.text = status.maxhp.ToString();
        hppoint.text = status.hp.ToString();
    }
}

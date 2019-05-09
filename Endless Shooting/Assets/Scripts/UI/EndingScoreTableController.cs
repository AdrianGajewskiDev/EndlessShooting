using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingScoreTableController : MonoBehaviour
{
    [SerializeField]GameObject[] enemies;
    [SerializeField]GameObject[] friends;
    [SerializeField]Text textToDisplay;
    [SerializeField]GameObject table;

    void Start()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        friends = GameObject.FindGameObjectsWithTag("PlayerTeam");
    }

    void Update()
    {
        if(GlobalScore.IsEndOfGame == true)
        {
            table.gameObject.SetActive(true);
            ShowWinners();
        }

    }
    void ShowWinners()
    {   
        
        textToDisplay.text = GlobalScore.Winners;
        
    }
    
}

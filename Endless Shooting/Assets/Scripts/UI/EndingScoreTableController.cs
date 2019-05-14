using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EndingScoreTableController : MonoBehaviour
{
    [SerializeField]  GameObject enemy_Table;
    [SerializeField]  GameObject friend_Table;
    [SerializeField]  GameObject draw_Table;
    [SerializeField]  TextMeshProUGUI endingScore;

    [Header("Draw")]
    [SerializeField]  TextMeshProUGUI enemyScore;
    [SerializeField]  TextMeshProUGUI friendScore;

    private GameObject currentTable;


    public void DisableTables()
    {
        enemy_Table.SetActive(false);
        friend_Table.SetActive(false);
        draw_Table.SetActive(false);
        endingScore.gameObject.SetActive(false);
        enemyScore.gameObject.SetActive(false);
        friendScore.gameObject.SetActive(false);
    }

    void Update()
    {
        if(GlobalScore.IsEndOfGame == true)
        {
            Time.timeScale = 0;
            ShowWinners();
            currentTable.SetActive(true);           
        }
    }
    void ShowWinners()
    {   
        if(GlobalScore.Winners == StaticStrings.EnemyTeamWon)
        {
            currentTable = enemy_Table;
            enemyScore.enabled = false;
            friendScore.enabled = false;
            endingScore.gameObject.SetActive(true);
            endingScore.text = new StringBuilder(GlobalScore.EnemyScore.ToString() + " / " + GlobalScore.FriendScore.ToString()).ToString();
        }
        else if(GlobalScore.Winners == StaticStrings.FriendTeamWon)
        {
            currentTable = friend_Table;
            enemyScore.enabled = false;
            friendScore.enabled = false;
            endingScore.gameObject.SetActive(true);
            endingScore.text = new StringBuilder(GlobalScore.EnemyScore.ToString() + " / " + GlobalScore.FriendScore.ToString()).ToString();
        }
        else if(GlobalScore.Winners == StaticStrings.Draw)
        {
            currentTable = draw_Table;
            endingScore.enabled = false;
            enemyScore.text = GlobalScore.EnemyScore.ToString();
            friendScore
                .text = GlobalScore.FriendScore.ToString();
            enemyScore.enabled = true;
            friendScore.enabled = true;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalScore : MonoBehaviour
{
    public Text enemyScoreText;
    public Text friendScoreText;

    public static int EnemyScore = 0;
    public static int FriendScore = 0;
    public static bool IsEndOfGame = false;
    public static string Winners;
    void Update()
    {
        Debug.Log(Time.timeScale);

        if(EnemyScore == 2 || FriendScore == 2)
        {
            if (EnemyScore > FriendScore)
                Winners = StaticStrings.EnemyTeamWon;
            else if (FriendScore > EnemyScore)
                Winners = StaticStrings.FriendTeamWon;
            else if (FriendScore == EnemyScore)
                Winners = StaticStrings.Draw;

            IsEndOfGame = true;
        }

        enemyScoreText.text = EnemyScore.ToString();
        friendScoreText.text = FriendScore.ToString();
    }

    public static void Restart()
    {
        IsEndOfGame = false;
        EnemyScore = 0;
        FriendScore = 0;
    }
    
}

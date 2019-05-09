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
        if(EnemyScore == 65 || FriendScore == 65)
        {
            if(EnemyScore > FriendScore)
                Winners = "Enemy Team Won";
            else if(FriendScore > EnemyScore)
                Winners = "You Team Won";


            IsEndOfGame = true;
        }

        


        enemyScoreText.text = EnemyScore.ToString();
        friendScoreText.text = FriendScore.ToString();
    }
}

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

    void Update()
    {
        enemyScoreText.text = EnemyScore.ToString();
        friendScoreText.text = FriendScore.ToString();
    }
}

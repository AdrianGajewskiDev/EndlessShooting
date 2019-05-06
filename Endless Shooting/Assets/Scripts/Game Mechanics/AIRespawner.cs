using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIRespawner : MonoBehaviour
{
    public Transform[] respawnPoints;
    public GameObject enemyPrefab;
    public GameObject friendPrefab;
    public int EnemyAmount;
    public int FriendAmount;
    public static int enemyAmount;
    public static int friendAmount;

    void Awake()
    {
        enemyAmount = EnemyAmount;
        friendAmount = FriendAmount;
    }
    // Update is called once per frame
    void Update()
    {
        if(enemyAmount < EnemyAmount)
        {
            var index = Random.Range(0, respawnPoints.Length);
            var respawnPoint = respawnPoints[index].position;
            Instantiate(enemyPrefab, respawnPoint, Quaternion.identity);
            enemyAmount += 1 ;
        }

        if(friendAmount < FriendAmount)
        {
            var index = Random.Range(0, respawnPoints.Length);
            var respawnPoint = respawnPoints[index].position;
            Instantiate(friendPrefab, respawnPoint, Quaternion.identity);
            friendAmount += 1;
        }
    }
}

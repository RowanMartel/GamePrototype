using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;
    GameObject[] enemies;

    void Start()
    {
        enemies = new GameObject[Global.enemyPoolCount];
        for (int i = 0; i < Global.enemyPoolCount; i++)
        {
            enemies[i] = Instantiate(enemyPrefab);
        }
    }

    void Update()
    {
        
    }
}
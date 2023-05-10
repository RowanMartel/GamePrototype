using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;
    GameObject[] enemies;

    float timer;
    [HideInInspector] public int active;
    [HideInInspector] public int kills;

    void Start()
    {
        timer = 0;
        kills = 0;
        enemies = new GameObject[Global.enemyPoolCount];
        for (int i = 0; i < Global.enemyPoolCount; i++)
        {
            active++;
            enemies[i] = Instantiate(enemyPrefab);
        }
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            timer = 0;
            if (active == 0)
            {
                enemies[0].GetComponent<Enemy>().GoActive();
                enemies[0].GetComponentInChildren<WeaponManager>().EquipEnemy(kills);
            }
        }
    }
}
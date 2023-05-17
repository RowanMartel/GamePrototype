using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    GameObject enemyPrefab;
    GameObject[] enemies;

    [SerializeField]
    List<Transform> spawns;

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
            if (active <= (kills / 2))
            {
                Transform spawn = spawns[Random.Range(0, spawns.Count)];
                for (int i = 0; i < enemies.Length; i++)
                {
                    if (enemies[i].GetComponent<Enemy>().State == Enemy.states.inactive)
                    {
                        enemies[i].GetComponent<Enemy>().GoActive(spawn);
                        enemies[i].GetComponentInChildren<WeaponManager>().EquipEnemy(kills);
                        break;
                    }
                }
            }
        }
    }
}
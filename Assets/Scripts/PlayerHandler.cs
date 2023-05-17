using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHandler : MonoBehaviour
{
    public int health;

    void Start()
    {
        health = Global.playerHpStart;
    }

    void Update()
    {
        
    }

    public void IsHit(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;
        if (health <= 0) Die();
    }

    void Die()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
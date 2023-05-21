using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerHandler : MonoBehaviour
{
    public int health;
    [SerializeField]
    TMP_Text healthText;
    string baseHealthText;

    void Start()
    {
        health = Global.playerHpStart;
        baseHealthText = healthText.text;
        healthText.text = baseHealthText + health;
    }

    void Update()
    {
        
    }

    public void IsHit(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;
        healthText.text = baseHealthText + health;
        if (health <= 0) Die();
    }

    void Die()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }
}
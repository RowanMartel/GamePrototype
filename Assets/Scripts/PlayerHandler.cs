using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerHandler : MonoBehaviour
{
    public int health;
    [SerializeField]
    TMP_Text healthText;
    string baseHealthText;
    Animator anim;
    TitleMenu titleMenu;

    // velocity
    Vector3 PrevPos;
    Vector3 NewPos;
    Vector3 ObjVelocity;

    void Start()
    {
        titleMenu = FindObjectOfType<TitleMenu>();
        PrevPos = transform.position;
        NewPos = transform.position;
        anim = GetComponentInChildren<Animator>();
        health = Global.playerHpStart;
        baseHealthText = healthText.text;
        healthText.text = baseHealthText + health;
    }

    private void Update()
    {
        anim.SetFloat("Speed", ObjVelocity.magnitude);
    }

    private void FixedUpdate()
    {
        NewPos = transform.position;  // each frame track the new position
        ObjVelocity = (NewPos - PrevPos) / Time.fixedDeltaTime;  // velocity = dist/time
        PrevPos = NewPos;  // update position for next frame calculation
    }

    public void IsHit(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;
        healthText.text = baseHealthText + health;
        if (health <= 0) Die();
        else anim.Play("Base Layer.Hurt");
    }

    public void Heal(int healAmount)
    {
        health += healAmount;
        if (health >= Global.playerHpStart)
            health = Global.playerHpStart;
        healthText.text = baseHealthText + health;
    }

    void Die()
    {
        anim.Play("Base Layer.Died");
        titleMenu.EndScreen();
    }
}
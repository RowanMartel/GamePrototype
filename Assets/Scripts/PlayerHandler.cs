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
    public static AudioSource audioSource;
    [SerializeField] AudioClip s_Hurt;
    [SerializeField] AudioClip s_Heal;
    [SerializeField] AudioClip s_GameOver;

    // velocity
    Vector3 PrevPos;
    Vector3 NewPos;
    Vector3 ObjVelocity;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
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
        else
        {
            anim.Play("Base Layer.Hurt");
            audioSource.PlayOneShot(s_Hurt);
        }
    }

    public void Heal(int healAmount)
    {
        audioSource.PlayOneShot(s_Heal);
        health += healAmount;
        if (health >= Global.playerHpStart)
            health = Global.playerHpStart;
        healthText.text = baseHealthText + health;
    }

    void Die()
    {
        audioSource.PlayOneShot(s_GameOver);
        anim.Play("Base Layer.Died");
        titleMenu.EndScreen();
    }

    public static void PlayClip(AudioClip s_Clip)
    {
        audioSource.PlayOneShot(s_Clip);
    }
}
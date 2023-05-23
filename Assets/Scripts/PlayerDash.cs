using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [HideInInspector] public float cooldown;
    [HideInInspector] public float dashSpeed;

    [SerializeField] float baseCooldown;
    [SerializeField] float baseDashSpeed;

    bool onCooldown;
    float timer;

    ExampleCharacterController controller;
    Animator anim;

    void Start()
    {
        onCooldown = false;
        controller = GetComponent<ExampleCharacterController>();
        anim = GetComponentInChildren<Animator>();
        Init();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && !onCooldown)
            Dash();

        if (onCooldown)
        {
            if (timer >= cooldown)
                onCooldown = false;
            timer += Time.deltaTime;
        }
    }

    void Dash()
    {
        anim.Play("Base Layer.DodgeRoll");
        controller.AddVelocity(transform.forward * dashSpeed);
        onCooldown = true;
        timer = 0;
    }

    public void Init()
    {
        cooldown = baseCooldown;
        dashSpeed = baseDashSpeed;
    }
}
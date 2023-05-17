using KinematicCharacterController.Examples;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [HideInInspector] public float cooldown;
    [HideInInspector] public float duration;
    [HideInInspector] public float dashSpeed;

    float baseWalkSpeed;
    [SerializeField] float baseCooldown;
    [SerializeField] float baseDuration;
    [SerializeField] float baseDashSpeed;

    bool onCooldown, dashing;
    float timer;

    ExampleCharacterController controller;

    void Start()
    {
        onCooldown = false;
        dashing = false;
        controller = GetComponent<ExampleCharacterController>();
        baseWalkSpeed = controller.MaxStableMoveSpeed;
        Init();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift)) && !onCooldown)
            Dash();

        if (dashing)
        {
            if (timer >= duration)
            {
                dashing = false;
                onCooldown = true;
                controller.MaxStableMoveSpeed = baseWalkSpeed;
            }
            timer += Time.deltaTime;
        }
        else if (onCooldown)
        {
            if (timer >= cooldown)
                onCooldown = false;
            timer += Time.deltaTime;
        }
    }

    void Dash()
    {
        Debug.Log("wefgag");
        controller.MaxStableMoveSpeed = dashSpeed;
        dashing = true;
        timer = 0;
    }

    public void Init()
    {
        duration = baseDuration;
        cooldown = baseCooldown;
        dashSpeed = baseDashSpeed;
    }
}
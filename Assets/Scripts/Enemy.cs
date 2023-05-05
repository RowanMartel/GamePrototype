using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    enum states
    {
        idle,
        walking,
        attacking,
        dying,
        dead,
        inactive
    }
    states state;
    states State 
    {
        get { return state; }
        set 
        {
            state = value;
            StateTransition(value);
        }
    }

    NavMeshAgent agent;
    Vector3 currentTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        State = states.walking;
    }

    void Update()
    {
        currentTarget = GetTarget();
        agent.SetDestination(currentTarget);
    }

    void StateTransition(states newState)
    {
        switch (newState)
        {
            case states.idle:
                break;
            case states.walking:
                break;
            case states.inactive:
                break;
            case states.dead:
                break;
            case states.attacking:
                break;
        }
    }

    Vector3 GetTarget()
    {
        if (State == states.walking)
            return Global.playerPos;
        else return transform.position;
    }
}
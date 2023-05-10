using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    int health;

    enum states
    {
        idle,
        chasing,
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
    [SerializeField] float attackRange;

    VisualWeapon weapon;
    EnemyManager manager;

    void Start()
    {
        manager = FindObjectOfType<EnemyManager>();
        agent = GetComponent<NavMeshAgent>();
        State = states.inactive;
        weapon = GetComponentInChildren<VisualWeapon>();
    }

    void Update()
    {
        currentTarget = GetTarget();
        agent.SetDestination(currentTarget);

        switch (State)
        {
            case states.idle:
                break;
            case states.chasing:
                if (InAttackRange()) State = states.attacking;
                break;
            case states.inactive:
                break;
            case states.dead:
                break;
            case states.attacking:
                if (!InAttackRange() && !weapon.isAttacking) State = states.chasing;
                else TryAttack();
                break;
            case states.dying:
                break;
        }
    }

    void TryAttack()
    {
        Vector3 _direction = (Global.playerPos - transform.position).normalized;
        Quaternion _lookRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 10);

        if (!weapon.isAttacking) weapon.DoAttackAnimation();
    }

    void StateTransition(states newState)
    {
        switch (newState)
        {
            case states.idle:
                manager.active++;
                State = states.chasing;
                break;
            case states.chasing:
                break;
            case states.inactive:
                manager.active--;
                GoInactive();
                break;
            case states.dead:
                manager.kills++;
                State = states.inactive;
                break;
            case states.attacking:
                break;
            case states.dying:
                State = states.dead;
                break;
        }
    }

    Vector3 GetTarget()
    {
        if (State == states.chasing)
            return Global.playerPos;
        else return transform.position;
    }

    public void IsHit(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            health = 0;
            State = states.dying;
        }
    }

    bool InAttackRange()
    {
        float difference = Vector3.Distance(transform.position, Global.playerPos);
        if (difference <= attackRange) return true;
        else return false;
    }

    public void GoActive()
    {
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
        State = states.idle;
    }
    void GoInactive()
    {
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }
}
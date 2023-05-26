using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    int health;
    int startingHealth = 50;

    float timer;
    bool attackWait;
    Animator anim;
    AudioSource audioSource;

    [SerializeField]
    GameObject itemDrop;

    // velocity
    Vector3 PrevPos;
    Vector3 NewPos;
    Vector3 ObjVelocity;

    public enum states
    {
        idle,
        chasing,
        attacking,
        dying,
        dead,
        inactive
    }
    states state;
    public states State 
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

    public VisualWeapon weapon;
    EnemyManager manager;
    GameManager gameManager;

    void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        manager = FindObjectOfType<EnemyManager>();
        agent = GetComponent<NavMeshAgent>();
        State = states.inactive;
        weapon = GetComponentInChildren<VisualWeapon>();
        transform.GetChild(0).gameObject.SetActive(true);
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        PrevPos = transform.position;
        NewPos = transform.position;
    }

    void Update()
    {
        anim.SetFloat("Speed", ObjVelocity.magnitude);

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
                else if (!attackWait)
                {
                    attackWait = true;
                    timer = 0;
                }
                break;
            case states.dying:
                break;
        }

        if (attackWait)
        {
            if (timer >= Global.enemyAtkTimer)
            {
                attackWait = false;
                TryAttack();
            }
            timer += Time.deltaTime;
        }
    }

    private void FixedUpdate()
    {
        NewPos = transform.position;  // each frame track the new position
        ObjVelocity = (NewPos - PrevPos) / Time.fixedDeltaTime;  // velocity = dist/time
        PrevPos = NewPos;  // update position for next frame calculation
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
                health = startingHealth;
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
                Score.modifyScore(5);
                manager.kills++;
                DropItem();
                State = states.inactive;
                break;
            case states.attacking:
                break;
            case states.dying:
                anim.Play("Base Layer.Hurt");
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
        else anim.Play("Base Layer.Hurt");
        audioSource.Play();
    }

    bool InAttackRange()
    {
        float difference = Vector3.Distance(transform.position, Global.playerPos);
        if (difference <= attackRange) return true;
        else return false;
    }

    public void GoActive(Transform spawn)
    {
        transform.position = spawn.position;
        transform.eulerAngles = spawn.eulerAngles;
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
        GetComponent<CapsuleCollider>().enabled = true;
        State = states.idle;
        transform.GetChild(0).gameObject.SetActive(true);
    }
    void GoInactive()
    {
        GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public Item PickSeedDrop()
    {
        Item seed;
        while (true)
        {
            seed = gameManager.Items[Random.Range(0, gameManager.Items.Count)];
            if (seed.tier <= manager.kills / 10 && seed.plantable)
                return seed;
        }
    }

    void DropItem()
    {
        GameObject drop = Instantiate(itemDrop, transform.position, transform.rotation);
        drop.GetComponent<Pickup>().item = PickSeedDrop();
        drop.GetComponent<Pickup>().Init();
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterAI : Entity
{
    NavMeshAgent nav;
    Transform player;

    [SerializeField] float sightRange;
    [SerializeField] float moveCycle = 1;
    float sightHeight = 1;
    float moveTime;
    public float dieTime = 4.5f;

    public Animator animator;
    public int attackDamage;
    public float attackDistance;

    #region Random Value
    float randomSelectX;
    float randomSelectZ;
    bool randomMove = false;
    #endregion

    public GameObject beforeDieEffect;
    public GameObject dieEffect;

    enum MonsterSate { Patrol, Chase, Attack, Die };
    [SerializeField] MonsterSate state = MonsterSate.Patrol;

    [Header("Gun Type")]
    public bool useGun = false;
    public GameObject shootEffect;
    public GameObject bullet;
    public Transform shootPoint;


    void Awake()
    {
        hp = maxHp;
        nav = GetComponent<NavMeshAgent>();
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if(Physics.Raycast(transform.position+new Vector3(0,0.5f,0), Vector3.down, 1))
        {
            nav.enabled = true;
        }
        if (state != MonsterSate.Die) 
        {
            RandomMoveTIme();
            MonsterApproch();
            MonsterActingState();
        }
        else if(dieTime > 0) dieTime -= Time.deltaTime;
        
        if (dieTime <= 0 && dieTime != 52) // 52 : no repeat
        {
            dieTime = 52;
            if (!useGun)
            {
                GameObject eff = Instantiate(dieEffect, transform.position, Quaternion.identity);
                Destroy(eff, eff.GetComponent<ParticleSystem>().main.duration);
            }
            Destroy(gameObject);
        }
    }

    #region MonsterAI_Setting
    void MonsterActingState()
    {
        if (hp != 0)
            switch (state)
            {
                case MonsterSate.Patrol:
                    RandomMove();
                    break;
                case MonsterSate.Chase:
                    if(nav.enabled)
                    nav.destination = player.position;
                    break;
                case MonsterSate.Attack:
                    transform.LookAt(player);
                    break;
            }
        else
        {
            if(beforeDieEffect)
                beforeDieEffect.SetActive(true);
            state = MonsterSate.Die;
            animator.SetTrigger("Die");
            if (nav.enabled)
            {
                nav.isStopped = true;
                nav.enabled = false;
            }
            GetComponent<Collider>().enabled = false;
        }
    }
    void MonsterApproch()
    {
        Vector3 start;
        if (useGun)
        {
            start = shootPoint.position;
        }
        else
            start = transform.position;

        Debug.DrawRay(start, Vector3.Normalize(player.position - start) * sightRange);
        RaycastHit hit;

        if (player.GetComponent<Player>().state != Player.PlayerCurrentState.Die && 
            Physics.Raycast(start + new Vector3(0,sightHeight,0), player.position - start, out hit, sightRange,1) 
            && hit.collider.gameObject.CompareTag("Player"))
        {
            if (Vector3.Magnitude(hit.point - start) <= attackDistance)
            {
                if (useGun && nav.enabled) 
                { 
                    nav.isStopped = true;
                }
                if (nav.enabled && animator && state != MonsterSate.Attack) AnimationPlay("Attack");
                state = MonsterSate.Attack;
            }
            else
            {
                if (useGun && nav.enabled) nav.isStopped = false;
                if (animator && state != MonsterSate.Chase) AnimationPlay("Walk");
                state = MonsterSate.Chase;
            }
        }
        else
        {
            if (useGun && nav.enabled) nav.isStopped = false;
            if (animator && state == MonsterSate.Attack) AnimationPlay("Walk");
            state = MonsterSate.Patrol;
        }
    }
    void RandomMove()
    {
        if (randomMove == false)
        {
            randomSelectX = Random.Range(-10, 10);
            if(randomSelectX >= 0 &&randomSelectX <= 5)
            {
                randomSelectX = 6;
            }
            else if (randomSelectX <= 0 && randomSelectX >= -5)
            {
                randomSelectX = -6;
            }
            randomSelectZ = Random.Range(-10, 10);
            if (randomSelectZ >= 0 && randomSelectZ <= 5)
            {
                randomSelectZ = 6;
            }
            else if (randomSelectZ <= 0 && randomSelectZ >= -5)
            {
                randomSelectZ = -6;
            }
            if(nav.enabled)
            nav.destination = transform.position + new Vector3(randomSelectX, 0, randomSelectZ);
            
            moveTime = moveCycle;
            randomMove = true;
        }
        else if (moveTime == 0)
        {
            randomMove = false;
        }
    }
    void RandomMoveTIme()
    {
        if (randomMove && moveTime != 0)
        {
            moveTime -= Time.deltaTime;
            if (moveTime < 0)
            {
                moveTime = 0;
            }
        }
    }
    void AnimationPlay(string name)
    {
        if (animator) animator.SetTrigger(name);
    }
    #endregion

    public void Shoot()
    {
        GameObject eff = Instantiate(shootEffect, shootPoint.position, Quaternion.identity);
        GameObject bullet_ = Instantiate(bullet, shootPoint.position, shootPoint.rotation);

        Destroy(eff, eff.GetComponent<ParticleSystem>().main.duration);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public static int score;

    // Player Animation
    [HideInInspector] public Animator pAnimator;
    public UI_Manager ui;
    public static bool rebirthEff = false;
    public static GameObject p; 
    public GameObject playerCamera;
    public GameObject hitEffect;

    // Player_Data
    //private float hitCycle = 0;
    public float effHeight= 0;

    public enum PlayerCurrentState
    {
        Normal, Reload, Shoot,Die
    }

    [Header("Player_State")]
    public PlayerCurrentState state = PlayerCurrentState.Normal;

    [Space]
    [Header("Player_Attack_Value")]
    public int AttackSpeed;
    public int Damage;

    [Space]
    [Header("Player_Movement")]
    public float h;
    public float v;
    public Player_Movement movement;
    public Player_Combat combat;

    public void RebirthEffectCheck()
    {
        rebirthEff = true;
    }

    void PlayerDie()
    {
        movement.enabled = false;
    }

    void MovementAnimatorSetting()
    {
        h = movement.horizontal;
        v = movement.vertical;

        pAnimator.SetFloat("H", h);
        pAnimator.SetFloat("V", v);

        if(h == 0 && v == 0)
        {
            pAnimator.SetBool("Walk",false);
            pAnimator.SetBool("Run", false);
        }
        else
        {
            pAnimator.SetBool("Walk", true);
            if (movement.currentSpeed == movement.runSpeed)
                pAnimator.SetBool("Run", true);
            else
                pAnimator.SetBool("Run", false);
        }
    }

    void Awake()
    {
        if (playerCamera) playerCamera.transform.parent = null;
        hp = maxHp;
        pAnimator = GetComponent<Animator>();
        pAnimator.SetBool("RespawnEff", rebirthEff);
        rebirthEff = false;
        movement = GetComponent<Player_Movement>();
        combat = GetComponent<Player_Combat>();

        p = this.gameObject;  
    }

    void Update()
    {
        if (hp == 0 && state != PlayerCurrentState.Die)
        {
            state = PlayerCurrentState.Die;
            pAnimator.SetTrigger("Die");
            PlayerDie();
        }
        MovementAnimatorSetting();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hp != 0 && other.gameObject.CompareTag("Child_Attack"))
        {
            GameObject eff = Instantiate(hitEffect, transform.position + new Vector3(0,effHeight,0),Quaternion.identity);
            if (other)
            {
                hp -= other.GetComponentInParent<MonsterAI>().attackDamage;
            }
            if (hp < 0) hp = 0;
            Destroy(eff, eff.GetComponent<ParticleSystem>().main.duration);
        }
        
    }
}
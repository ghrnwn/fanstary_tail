using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Combat : MonoBehaviour
{
    public Player player;

    public Transform shoot;
    public Transform shootPoint;
    public GameObject shootEffect;
    public GameObject bullet;

    private void Awake()
    {
        player = GetComponent<Player>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0) && player.state == Player.PlayerCurrentState.Normal)
        {
            player.state = Player.PlayerCurrentState.Shoot;
            player.pAnimator.SetTrigger("Shoot");
        }
    }
    public void Shoot()
    {
        GameObject eff = Instantiate(shootEffect, shootPoint.position, Quaternion.identity);
        GameObject bullet_ = Instantiate(bullet, shootPoint.position, shootPoint.rotation);

        Destroy(eff, eff.GetComponent<ParticleSystem>().main.duration);
    }
    public void ShootEnd()
    {
        player.state = Player.PlayerCurrentState.Normal;
    }
}

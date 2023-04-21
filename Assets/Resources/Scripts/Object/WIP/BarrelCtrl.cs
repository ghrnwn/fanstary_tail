using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelCtrl : MonoBehaviour
{
    public GameObject expEffect;
    Transform tr;

    public int hitCount = 0;
    public int damage = 50;

    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.GetComponent<BulletAttack>() && coll.gameObject.GetComponent<BulletAttack>().target == "Enemy")
        {
            Destroy(coll.gameObject);
            if(++hitCount >= 2)
            {
                ExpBarrel();
            }
        }    
    }
    void ExpBarrel()
    {
        GameObject explosion = Instantiate(expEffect,transform.position,Quaternion.identity);
        Collider[] colls = Physics.OverlapSphere(tr.position, 7.0f);
        foreach(Collider coll in colls)
        {
            if(coll.CompareTag("Enemy"))
                coll.GetComponent<Entity>().OnDamaged(damage);
        }
        Player.score += 50;
        Destroy(explosion, explosion.GetComponent<ParticleSystem>().main.duration);
        Destroy(gameObject);
    }
}

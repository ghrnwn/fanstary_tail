using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    public int damage;
    public int speed;
    Rigidbody r;

    public GameObject owner;
    public GameObject eff;
    public string target = "Enemy";
    public BulletAttack(int dam)
    {
        damage = dam;
    }
    // Start is called before the first frame update
    void Start()
    {
        r = GetComponent<Rigidbody>();
        Destroy(gameObject, 5f);

        r.AddForce(transform.forward * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Transparent") && (other.gameObject != owner || !owner))
        {
            if (other.gameObject.GetComponent<BulletAttack>()
            && target == other.gameObject.GetComponent<BulletAttack>().target)
            {
                return;
            }
            GameObject effect = Instantiate(eff, transform.position, Quaternion.identity);
            Destroy(effect, effect.GetComponent<ParticleSystem>().main.duration);

            if (other.gameObject.CompareTag(target))
            {
                other.GetComponent<Entity>().OnDamaged(damage);
            }
            Destroy(gameObject);
        }
    }
}

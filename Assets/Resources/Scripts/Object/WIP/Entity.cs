using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] public int hp;
    [SerializeField] public int maxHp = 100;
    public bool invincible = false;

    public void OnDamaged(int damage)
    {
        if (invincible) return;
        hp -= damage;
        if (hp <= 0)
        {
            if (gameObject != Player.p) Player.score += 200;
            hp = 0;
        }
    }
    public void GroundSlopeChecker()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position - new Vector3(0, -1, 0), Vector3.down, out hit, 2f))
        {
            float change_Y_Value = transform.localEulerAngles.y;
            transform.up = hit.normal;
            transform.Rotate(Vector3.up * (change_Y_Value));
        }
    }
}

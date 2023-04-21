using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingPotion : MonoBehaviour
{
    [SerializeField]
    private GameObject healSoundObj;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
           
            Player player = other.GetComponent<Player>(); 
            player.hp = player.maxHp;
            Player.score += 100;

            Instantiate(healSoundObj, this.transform.position, this.transform.rotation);

            Destroy(transform.parent.gameObject);
            Destroy(transform.root.gameObject);
            gameObject.SetActive(false); 
        }
    }
}

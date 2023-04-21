using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSystem : MonoBehaviour
{
    AudioSource musicPlayer;
    Animator ani;
    public int savePoint = 0;
    private void Awake()
    {
        musicPlayer = GetComponent<AudioSource>();
        ani = GetComponent<Animator>();
    }

    private void OnTriggerStay(Collider collision)
    {
        if (ani && collision.gameObject.CompareTag("Player"))
        {
            ani.SetBool("character_nearby", true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (MenuAndPause.saveProgress <= savePoint)
        {
            MenuAndPause.saveProgress = savePoint;
        }
        if (ani && collision.gameObject.CompareTag("Player"))
        {
            ani.SetBool("character_nearby", false);
        }
    }

    public void PlaySfx(AudioClip sound)
    {
        if (musicPlayer) musicPlayer.PlayOneShot(sound);
    }
    public void Lock()
    {
        ani.SetBool("character_nearby", false);
    }
}

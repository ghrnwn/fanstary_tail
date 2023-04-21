using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealSound : MonoBehaviour
{
    [SerializeField]
    private AudioSource healSound;
    private void Awake()
    {
        healSound = GetComponent<AudioSource>();
    }
    private void Start()
    {
        healSound.Play();
    }

    
}

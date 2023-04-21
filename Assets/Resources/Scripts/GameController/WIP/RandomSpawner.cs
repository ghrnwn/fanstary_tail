using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RandomSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject[] spawnPrefab;
    public GameObject spawnEffect;
    public AudioSource backgroundMusic;
    
    [Space]
    [Header("Spawn Settings")]
    public float RangeX1 = -72.1f;
    public float RangeX2 = -53.1f;
    public float Y = 3f;
    public float Range1Z = -5f;
    public float Range2Z = 23f;
    public float timer = 120f;

    private int i = 0;
    private int itemTimer = 0;
    private int itemFixPos = 8;

    Entity player;
    
    // Update is called once per frame

    private void Start()
    {
        if(backgroundMusic) backgroundMusic.enabled = false;
        GetComponent<AudioSource>().enabled = true;
        if(Player.p) player = Player.p.GetComponent<Entity>();
        StartCoroutine(spawnSomeThing());
    }
    void Update()
    {
        if (player && player.hp <= 0) return; 
        if (timer >= 0)
            timer -= Time.deltaTime;

        if (timer <= 0)
        {
            i = 1;
        }
    }
    IEnumerator spawnSomeThing()
    {
        while (i == 0)
        {
            yield return new WaitForSeconds(1f);

            if (player && player.hp <= 0) yield break;
            Vector3 spawnPos = new Vector3(Random.Range(RangeX1, RangeX2), Y, Random.Range(Range1Z, Range2Z));
            int Index = Random.Range(0, spawnPrefab.Length-1);
            Instantiate(spawnPrefab[Index], spawnPos, spawnPrefab[Index].transform.rotation);
            Instantiate(spawnEffect, spawnPos, spawnPrefab[Index].transform.rotation);
            itemTimer++;
            if(itemTimer >=30)
            {
                spawnPos.y -= itemFixPos;
                Instantiate(spawnPrefab[3], spawnPos, spawnPrefab[Index].transform.rotation);
                itemTimer = 0;
            }
        }
        foreach (Entity ent in FindObjectsOfType<Entity>())
        {
            if (ent != player) ent.hp = 0;
        }
        StartCoroutine(Player.p.GetComponent<Player>().ui.GameClear());
        yield break;

    }
}

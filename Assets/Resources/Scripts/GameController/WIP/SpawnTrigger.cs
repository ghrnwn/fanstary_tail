using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    
    [SerializeField]
    RandomSpawner spawner;
    public DoorSystem door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.GetComponent<Player>().playerCamera.GetComponent<Camera_Tracer>().first_forwardDistance = 5;
            spawner.enabled = true;
            door.Lock();
            Destroy(door);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomController : MonoBehaviour
{
    private Transform[] doors;
    private Transform[] spawners;
    private bool spawnersEnabled = false;
    private int spawnersLeft = 0;

    void Awake()
    {
        EventHandler.TriggerSpawnerClear += SpawnerCleared;
        var children = gameObject.GetComponentsInChildren<Transform>();

        doors = children.Where(child => child.tag == "Door").ToArray();
        spawners = children.Where(child => child.tag == "Spawner").ToArray();
        spawnersLeft = spawners.Count();
        
        foreach (Transform door in doors)
        {
            door.gameObject.SetActive(false);
        }
    }

    private void SpawnerCleared(int id){
        if(id == transform.GetInstanceID()){
        spawnersLeft--;
        if(spawnersLeft < 1){
            TriggerDoor();
        }
        }
    }

    private void TriggerDoor()
    {
        foreach (Transform door in doors)
        {
            AudioManager.Instance.PlaySFX("DoorCreak");
            door.gameObject.SetActive(!door.gameObject.activeSelf);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !spawnersEnabled)
        {
            spawnersEnabled = true;
            foreach (Transform spawner in spawners)
            {
                AudioManager.Instance.PlaySFX("DoorCreak");
                spawner.GetComponent<EnemySpawner>().Spawn();
            }

            TriggerDoor();
        }
    }
}
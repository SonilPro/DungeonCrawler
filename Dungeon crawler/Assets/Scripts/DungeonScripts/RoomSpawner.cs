using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : MonoBehaviour
{
    /*RoomTemplates templates;
    public int openingDirection;
    public bool spawned;
    public int rand;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        Invoke("Spawn", 0.4f);
    }


    public void Spawn()
    {

        if (spawned == false)
        {
            CancelInvoke("Spawn");
            if (openingDirection == 1)
            {
                // Need to spawn a room with a BOTTOM door.
                rand = Random.Range(0, templates.bottomRooms.Length);
                Instantiate(templates.bottomRooms[rand], transform.position, templates.bottomRooms[rand].transform.rotation);

            }
            else if (openingDirection == 2)
            {
                // Need to spawn a room with a TOP door.

                rand = Random.Range(0, templates.topRooms.Length);
                Instantiate(templates.topRooms[rand], transform.position, templates.topRooms[rand].transform.rotation);

            }
            else if (openingDirection == 3)
            {
                // Need to spawn a room with a LEFT door.
                rand = Random.Range(0, templates.leftRooms.Length);
                Instantiate(templates.leftRooms[rand], transform.position, templates.leftRooms[rand].transform.rotation);

            }
            else if (openingDirection == 4)
            {
                // Need to spawn a room with a RIGHT door.
                rand = Random.Range(0, templates.rightRooms.Length);
                Instantiate(templates.rightRooms[rand], transform.position, templates.rightRooms[rand].transform.rotation);

            }
            spawned = true;
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("SpawnPoint"))
        {
            Debug.Log("SpawnPoint");
            Invoke("DestroySP", 0.1f);
        }


        else
        {
            Invoke("DestroySP", 0.1f);
        }

    }
    void DestroySP()
    {
        Destroy(gameObject);
    }
*/
}




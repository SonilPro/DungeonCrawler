using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    RoomTemplates templates;
    bool spawned = false;
    GameObject child;

    void Start()
    {
        templates = RoomTemplates.Instance;
        Invoke("Spawn", 0.2f);
    }

    void Spawn()
    {
        if (spawned == false)
        {
            int random = Random.Range(0, templates.roomTemplates.Length);
            Instantiate(templates.roomTemplates[random], transform.position, templates.roomTemplates[random].transform.rotation);
        }
        spawned = true;
    }

}
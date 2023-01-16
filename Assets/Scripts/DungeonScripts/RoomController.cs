using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RoomController : MonoBehaviour
{
    private Transform[] doors;
    void Awake()
    {
        EventHandler.TriggerDoorStateEvent += TriggerDoor;

        doors = gameObject.GetComponentsInChildren<Transform>();
        doors = doors.Where(child => child.tag == "Door").ToArray();
    }

    private void TriggerDoor(int id)
    {
        if (id == transform.GetInstanceID())
        {
            foreach (Transform door in doors)
            {
                door.gameObject.SetActive(!door.gameObject.activeSelf);
            }
        }
    }
}
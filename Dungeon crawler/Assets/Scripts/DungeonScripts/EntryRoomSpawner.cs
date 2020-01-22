using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntryRoomSpawner : MonoBehaviour
{

    /*    private int rand;
        private RoomTemplates templates;
        public bool oneDoorRoom = false;
        public GameObject room;
        private int child;

        private void Start()
        {
            templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
            Invoke("Spawn", 0.1f);

        }

        private void Spawn()
        {
            CancelInvoke("Spawn");
            rand = Random.Range(0, templates.entryRooms.Length);
            room = Instantiate(templates.entryRooms[rand], transform.position, templates.entryRooms[rand].transform.rotation);
            Invoke("Destroy", 0.5f);





        }
        void Destroy()
        {
            foreach (Transform child in room.transform)
            {
                Destroy(child.gameObject);


            }

        }
        */

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);
    }

    private void TriggerDoor()
    {
        Debug.Log("triggerM");
        gameObject.SetActive(!transform.gameObject.activeSelf);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    void FixedUpdate()
    {
        transform.position = player.transform.position;
    }

}

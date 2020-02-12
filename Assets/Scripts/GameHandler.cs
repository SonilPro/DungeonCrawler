using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public CameraController cameraController;
    public PlayerController playerController;
    public Transform player;
    

    private void Start()
    {
        cameraController.Setup(() => player.transform.position);
        
    }
}

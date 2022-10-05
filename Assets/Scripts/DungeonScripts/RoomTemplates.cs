using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTemplates : MonoBehaviour
{
    public static RoomTemplates Instance { get; private set; }
    void Awake()
    {
        Instance = this;
    }

    public GameObject[] roomTemplates;
}
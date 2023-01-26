using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHandler : MonoBehaviour
{
    public static event Action<int> TriggerSpawnerClear;

    public static void StartSpawnerClearEvent(int id)
    {
        TriggerSpawnerClear?.Invoke(id);
    }
}

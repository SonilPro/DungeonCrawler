using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHandler : MonoBehaviour
{
    public static event Action<int> TriggerDoorStateEvent;

    public static void StartDoorEvent(int id)
    {
        TriggerDoorStateEvent?.Invoke(id);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EventHandler : MonoBehaviour
{
    public static event Action TriggerDoorStateEvent;

    public static void StartDoorEvent()
    {
        TriggerDoorStateEvent?.Invoke();
    }
}

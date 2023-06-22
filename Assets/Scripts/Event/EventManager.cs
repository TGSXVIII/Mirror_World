using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public partial class EventManager : MonoBehaviour
{
    private static Dictionary<EventName, UnityEvent> events = new();

    public static void StartListening(EventName name, UnityAction handler)
    {
        if (!events.TryGetValue(name, out UnityEvent thisEvent))
        {
            thisEvent = new UnityEvent();
            events.Add(name, thisEvent);
        }

        thisEvent.AddListener(handler);
    }

    public static void StopListening(EventName name, UnityAction handler)
    {
        if (events.TryGetValue(name, out UnityEvent thisEvent))
        {
            thisEvent.RemoveListener(handler);
        }
    }

    public static void Fire(EventName name)
    {
        if (events.TryGetValue(name, out UnityEvent thisEvent))
        {
            thisEvent.Invoke();
        }
    }
}



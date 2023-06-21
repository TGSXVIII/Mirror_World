using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class SingleParamEvent : UnityEvent<object> { }

public partial class EventManager
{
    private static Dictionary<EventName, SingleParamEvent> singleParamEvents = new();

    public static void StartListening(EventName name, UnityAction<object> handler)
    {
        if (!singleParamEvents.TryGetValue(name, out SingleParamEvent thisEvent))
        {
            thisEvent = new SingleParamEvent();
            singleParamEvents.Add(name, thisEvent);
        }

        thisEvent.AddListener(handler);
    }

    public static void StopListening(EventName name, UnityAction<object> handler)
    {
        if (singleParamEvents.TryGetValue(name, out SingleParamEvent thisEvent))
        {
            thisEvent.RemoveListener(handler);
        }
    }

    public static void Fire(EventName name, object param)
    {
        if (singleParamEvents.TryGetValue(name, out SingleParamEvent thisEvent))
        {
            thisEvent.Invoke(param);
        }
    }
}


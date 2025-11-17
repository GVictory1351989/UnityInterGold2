using System;
using System.Collections.Generic;
using UnityEngine;

public static class EventManagerUtils
{
    private static readonly Dictionary<string, Delegate> Events = new Dictionary<string, Delegate>();
    public static void Subscribe<T>(string key, Action<T> callback)
    {
        if (Events.ContainsKey(key))
        {
            Events[key] = Delegate.Combine(Events[key], callback);
        }
        else
        {
            Events.Add(key, callback);
        }
    }

    public static void Unsubscribe<T>(string key, Action<T> callback)
    {
        if (Events.ContainsKey(key))
        {
            Events[key] = Delegate.Remove(Events[key], callback);

            if (Events[key] == null)
            {
                Events.Remove(key);
            }
        }
    }

    public static void RaiseEvent<T>(string key, T eventData)
    {
        if (Events.TryGetValue(key, out var del))
        {
            if (del is Action<T> callback)
            {
                callback.Invoke(eventData);
            }
            else
            {
                Debug.LogError($"Event '{key}' exists but has a different type.");
            }
        }
    }

    public static void ClearEvents()
    {
        Events.Clear();
    }
}

public class GameEvent<T>
{
    public T Data { get; }
    public Action Callback { get; }

    public GameEvent(T data, Action callback = null)
    {
        Data = data;
        Callback = callback;
    }
}

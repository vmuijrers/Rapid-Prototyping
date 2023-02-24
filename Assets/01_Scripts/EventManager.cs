using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Add you message Types here
public enum MessageEnum
{
    NONE = 0,
}

public static class EventManager
{
    private static readonly Dictionary<MessageEnum, Action> messages = new Dictionary<MessageEnum, Action>();

    public static void SendMessage(MessageEnum ev)
    {
        if (ev == MessageEnum.NONE)
        {
            Debug.LogWarning("Event was NONE when adding Listener!");
            return;
        }
        if (messages.ContainsKey(ev))
        {
            messages[ev]?.Invoke();
        }
    }

    public static void AddListener(MessageEnum ev, Action action)
    {
        if (ev == MessageEnum.NONE)
        {
            Debug.LogWarning("Event was NONE when adding Listener!");
            return;
        }
        if (!messages.ContainsKey(ev))
        {
            messages.Add(ev, null);
        }
        messages[ev] += action;
    }

    public static void RemoveListener(MessageEnum ev, Action action)
    {
        if (ev == MessageEnum.NONE)
        {
            Debug.Log("Event was NONE when adding Listener!");
            return;
        }
        if (messages.ContainsKey(ev))
        {
            messages[ev] -= action;
            if (messages[ev] == null)
            {
                messages.Remove(ev);
            }
        }
    }
}

public static class EventManager<T>
{
    private static readonly Dictionary<MessageEnum, Action<T>> messages = new Dictionary<MessageEnum, Action<T>>();

    public static void SendMessage(MessageEnum ev, T arg)
    {
        if (ev == MessageEnum.NONE)
        {
            Debug.Log("Event was NONE when adding Listener!");
            return;
        }
        if (messages.ContainsKey(ev))
        {
            messages[ev]?.Invoke(arg);
        }
    }

    public static void AddListener(MessageEnum ev, Action<T> action)
    {

        if (ev == MessageEnum.NONE)
        {
            Debug.Log("Event was NONE when adding Listener!");
            return;
        }
        if (!messages.ContainsKey(ev))
        {
            messages.Add(ev, null);
        }
        messages[ev] += action;
    }

    public static void RemoveListener(MessageEnum ev, Action<T> action)
    {
        if (ev == MessageEnum.NONE)
        {
            Debug.Log("Event was NONE when adding Listener!");
            return;
        }
        if (messages.ContainsKey(ev))
        {
            messages[ev] -= action;
            if (messages[ev] == null)
            {
                messages.Remove(ev);
            }
        }
    }
}

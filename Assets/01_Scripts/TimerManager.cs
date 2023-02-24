using UnityEngine;
using System.Collections.Generic;

public class TimerManager : MonoBehaviour
{
    private static List<Timer> timers = new List<Timer>();

    public static Timer StartTimer(float duration, string name = "SomeTimer", System.Action<float> OnUpdate = null, System.Action OnDone = null)
    {
        Timer t = new Timer(duration, name, OnUpdate, OnDone);
        StartTimer(t);
        return t;
    }

    public static void StartTimer(Timer timer)
    {
        timer.Restart();
    }

    public static void ClearAllTimers()
    {
        timers.Clear();
    }

    public void FixedUpdate()
    {
        if (timers.Count != 0)
        {
            for (int i = timers.Count - 1; i >= 0; i--)
            {
                if (timers[i] == null) { timers.RemoveAt(i); }
                timers[i].OnUpdate();
            }
        }
    }

    public class Timer
    {
        public string name = "Timer_";
        public float duration;
        public float maxDuration;
        public System.Action<float> OnUpdateEvent;
        public System.Action OnDoneEvent;

        public Timer(float maxDuration, string name = "Timer_", System.Action<float> OnUpdate = null, System.Action OnDone = null)
        {
            this.name = name;
            duration = 0;
            this.maxDuration = maxDuration;
            OnUpdateEvent = OnUpdate;
            OnDoneEvent = () =>
            {
                timers.Remove(this);
            };
            OnDoneEvent += OnDone;
        }

        public void OnUpdate()
        {
            if (duration > 0)
            {
                duration -= Time.deltaTime;
                OnUpdateEvent?.Invoke(GetPercentageDone());
                if (duration <= 0)
                {
                    duration = 0;
                    OnDoneEvent?.Invoke();
                }
            }
        }

        public string LogTimer()
        {
            return "Timer " + name + ": " + duration.ToString() + "/" + maxDuration.ToString();
        }

        public float GetPercentageDone()
        {
            return 1f - (duration / maxDuration);
        }
        public void Restart()
        {
            duration = maxDuration;
            if (!timers.Contains(this))
            {
                timers.Add(this);
            }
        }

        public bool IsActive()
        {
            return duration > 0;
        }
    }
}


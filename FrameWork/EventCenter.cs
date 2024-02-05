using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


    public interface IEventInfo { }
    public class EventInfo : IEventInfo
    {
        public UnityAction action;
        public EventInfo(UnityAction _action)
        {
            action += _action;
        }
    }
    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> action;
        public EventInfo(UnityAction<T> _action)
        {
            action = _action;
        }
    }
    public class EventInfo<T1, T2> : IEventInfo
    {
        public UnityAction<T1, T2> action;
        public EventInfo(UnityAction<T1, T2> _action)
        {
            action = _action;
        }
    }
    public class EventCenter : Singleton<EventCenter>
    {
        private Dictionary<string, IEventInfo> eventDic = new Dictionary<string, IEventInfo>();
        public void AddListener(string eventName, UnityAction callback)
        {
            if (eventDic.ContainsKey(eventName))
            {
                (eventDic[eventName] as EventInfo).action += callback;
            }
            else
            {
                eventDic.Add(eventName, new EventInfo(callback));
            }
        }
        public void AddListener<T>(string eventName, UnityAction<T> callback)
        {
            if (eventDic.ContainsKey(eventName))
            {
                (eventDic[eventName] as EventInfo<T>).action += callback;
            }
            else
            {
                eventDic.Add(eventName, new EventInfo<T>(callback));
            }
        }
        public void AddListener<T1, T2>(string eventName, UnityAction<T1, T2> callback)
        {
            if (eventDic.ContainsKey(eventName))
            {
                (eventDic[eventName] as EventInfo<T1, T2>).action += callback;
            }
            else
            {
                eventDic.Add(eventName, new EventInfo<T1, T2>(callback));
            }
        }
        public void RemoveListener(string eventName, UnityAction callback)
        {
            if (eventDic.ContainsKey(eventName))
            {
                (eventDic[eventName] as EventInfo).action -= callback;
            }
        }
        public void RemoveListener<T>(string eventName, UnityAction<T> callback)
        {
            if (eventDic.ContainsKey(eventName))
            {
                (eventDic[eventName] as EventInfo<T>).action -= callback;
          //  Debug.Log("移除成功" + eventName);
            }
        }
        public void RemoveListener<T1, T2>(string eventName, UnityAction<T1, T2> callback)
        {
            if (eventDic.ContainsKey(eventName))
            {
                (eventDic[eventName] as EventInfo<T1, T2>).action -= callback;
            }
        }
        public void EventTrigger(string eventName)
        {
            if (eventDic.ContainsKey(eventName))
            {
                (eventDic[eventName] as EventInfo).action?.Invoke();
            }
        }
        public void EventTrigger<T>(string eventName, T info)
        {
            if (eventDic.ContainsKey(eventName))
            {
                (eventDic[eventName] as EventInfo<T>).action?.Invoke(info);
            }
        }
        public void EventTrigger<T1, T2>(string eventName, T1 info1, T2 info2)
        {
            if (eventDic.ContainsKey(eventName))
            {
                (eventDic[eventName] as EventInfo<T1, T2>).action?.Invoke(info1, info2);
            }
        }
        public void Clear()
        {
            eventDic.Clear();
        }
    }


using System;
using UnityEngine;
using UnityEngine.Events;


    public class MonoManager : MonoSingleton<MonoManager>
    {
        private UnityAction updateCallBack;
        private void Update()
        {
            updateCallBack?.Invoke();
        }
        public void AddListener(UnityAction callback)
        {
            updateCallBack += callback;
        }
        public void RemoveListener(UnityAction callback)
        {
            updateCallBack -= callback;
        }

    }


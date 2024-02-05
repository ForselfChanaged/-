using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


    public class ObjectPool : Singleton<ObjectPool>
    {
        private Dictionary<string, Stack<GameObject>> pool;
        protected override void Init()
        {
            base.Init();
            pool = new Dictionary<string, Stack<GameObject>>();
        }
        public GameObject GetGameObject(string name,GameObject prefab)
        {
            GameObject obj = null;
            if(pool.ContainsKey(name))
            {
                if(pool[name].TryPop(out obj))
                {
                    obj.SetActive(true);
                    return obj;
                }
            }
            obj = GameObject.Instantiate(prefab);
            return obj;
        }
        public void RecycleGameObject(string name, GameObject obj)
        {
            obj.SetActive(false);
            if (!pool.ContainsKey(name))
                pool.Add(name, new Stack<GameObject>());
            pool[name].Push(obj);

        }
        public void DelayRecycle(string name, GameObject obj, float time)
        {
            MonoManager.Instance.StartCoroutine(ReallyRecycle(name, obj, time));
        }
        public void Clear()
        {
            foreach (var s in pool.Values)
            {
                s.Clear();
            }
            pool.Clear();
        }
        private IEnumerator ReallyRecycle(string name, GameObject obj, float time)
        {
            yield return new WaitForSeconds(time);
            RecycleGameObject(name, obj);
        }
    }


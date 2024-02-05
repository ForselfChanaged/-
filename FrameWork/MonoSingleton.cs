using UnityEngine;


    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    GameObject o = new GameObject("MonoSingleton of " + typeof(T));
                    o.AddComponent<T>();
                }
                return instance;
            }
        }
        private void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                Init();
            }
        }
        protected virtual void Init()
        {
            GameObject.DontDestroyOnLoad(this);
        }

    }




